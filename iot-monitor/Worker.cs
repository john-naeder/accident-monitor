using System.Text.Json;
using Microsoft.Extensions.Options;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using iot_monitor.Application.Interfaces;
using iot_monitor.Infrastructure.InfluxDb;
using iot_monitor.Infrastructure.MQTT;

namespace IotMonitorService
{
    public class Worker(ILogger<Worker> logger,
                  IMqttService mqttService,
                  IServiceProvider serviceProvider,
                  IOptions<InfluxDbSettings> influxDbSettings) : BackgroundService
    {
        private readonly InfluxDbSettings _influxDbSettings = influxDbSettings.Value;

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Worker starting.");

            if (mqttService == null)
            {
                logger.LogError("IMqttService was not resolved correctly.");
                throw new InvalidOperationException("IMqttService was not resolved correctly.");
            }
            mqttService.OnMessageReceivedAsync += HandleMqttMessageAsync;

            var startResult = await mqttService.StartAsync(cancellationToken);
            if (!startResult.IsSuccess)
            {
                logger.LogError("Failed to start MQTT Service: {Message}. Worker will not process messages.", startResult.Message);
                 throw new Exception($"Failed to start MQTT Service: {startResult.Message}");
            }
            else
            {
                logger.LogInformation("MQTT Service started successfully by Worker.");
            }

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
        }

        private async Task HandleMqttMessageAsync(string topic, string payload)
        {
            logger.LogInformation("Worker received message via event on topic '{topic}': {payload}", topic, payload);
            using var scope = serviceProvider.CreateScope();
            var scopedLogger = scope.ServiceProvider.GetRequiredService<ILogger<Worker>>();
            var influxDbClient = scope.ServiceProvider.GetRequiredService<InfluxDBClient>();

            if (influxDbClient == null)
            {
                scopedLogger.LogError("InfluxDBClient could not be resolved within the message handler scope.");
                return;
            }

            try
            {
                var deviceId = TopicParser.ExtractId("iot/rsu/{id}", topic);
                if (deviceId != null)
                {
                    using var jsonDoc = JsonDocument.Parse(payload);
                    var root = jsonDoc.RootElement;

                    var point = PointData.Measurement("device_status")
                                        .Tag("deviceId", deviceId)
                                        .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

                    if (root.TryGetProperty("isOnline", out var onlineElement) && (onlineElement.ValueKind == JsonValueKind.True || onlineElement.ValueKind == JsonValueKind.False))
                        point = point.Field("isOnline", onlineElement.GetBoolean());
                    if (root.TryGetProperty("batteryLevel", out var batteryElement) && (batteryElement.ValueKind == JsonValueKind.Number))
                        point = point.Field("batteryLevel", batteryElement.GetDouble());
                    if (root.TryGetProperty("signalStrength", out var signalElement) && (signalElement.ValueKind == JsonValueKind.Number))
                        point = point.Field("signalStrength", signalElement.GetDouble());
                    if (root.TryGetProperty("location", out var locationElement) && (locationElement.ValueKind == JsonValueKind.Object))
                    {
                        if (locationElement.TryGetProperty("latitude", out var latElement) && latElement.ValueKind == JsonValueKind.Number)
                            point = point.Field("latitude", latElement.GetDouble());
                        if (locationElement.TryGetProperty("longitude", out var lonElement) && lonElement.ValueKind == JsonValueKind.Number)
                            point = point.Field("longitude", lonElement.GetDouble());
                    }
                    if (root.TryGetProperty("address", out var addressElement) && (addressElement.ValueKind == JsonValueKind.String))
                        point = point.Field("address", addressElement.GetString());

                    if (point.HasFields())
                    {
                        await influxDbClient.GetWriteApiAsync().WritePointAsync(point, _influxDbSettings.Bucket, _influxDbSettings.Org);
                        scopedLogger.LogInformation("[Scope-{ScopeId}] Wrote status for device '{deviceId}' to InfluxDB.", scope.GetHashCode(), deviceId);
                    }
                    else
                    {
                        scopedLogger.LogWarning("[Scope-{ScopeId}] No valid fields found for device '{deviceId}'. Skipping InfluxDB write.", scope.GetHashCode(), deviceId);
                    }
                }
                else
                {
                    scopedLogger.LogWarning("[Scope-{ScopeId}] Received message on unhandled topic format: {topic}", scope.GetHashCode(), topic);
                }
            }
            catch (JsonException jsonEx)
            {
                scopedLogger.LogError(jsonEx, "[Scope-{ScopeId}] Failed to parse JSON payload from topic '{topic}'. Payload: {payload}", scope.GetHashCode(), topic, payload);
            }
            catch (Exception ex)
            {
                scopedLogger.LogError(ex, "[Scope-{ScopeId}] Error processing message from topic '{topic}' or writing to InfluxDB.", scope.GetHashCode(), topic);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Worker stopping.");

            if (mqttService != null)
            {
                mqttService.OnMessageReceivedAsync -= HandleMqttMessageAsync;
            }

            await base.StopAsync(cancellationToken);
        }
    }
}