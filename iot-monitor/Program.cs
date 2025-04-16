using iot_monitor.Infrastructure.MQTT;
using iot_monitor.Infrastructure.InfluxDb;
using IotMonitorService;
using InfluxDB.Client;
using Microsoft.Extensions.Options;
using AccidentMonitor.Infrastructure.MQTT;
using iot_monitor.Application.Interfaces;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);


builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.Configure<MqttConnectionConfiguration>(builder.Configuration.GetSection("MqttSettings"));
builder.Services.Configure<InfluxDbSettings>(builder.Configuration.GetSection("InfluxDbSettings"));

builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<InfluxDbSettings>>().Value;
    if (string.IsNullOrEmpty(options.Url) || string.IsNullOrEmpty(options.Token))
    {
        var logger = sp.GetRequiredService<ILogger<Program>>();
        logger.LogError("InfluxDB URL or Token is not configured properly.");
        throw new InvalidOperationException("InfluxDB URL or Token is not configured.");
    }
    // var influxDBClient = new InfluxDBClient(options.Url, options.Token);
    //var health = await influxDBClient.PingAsync();
    //if (!health)
    //{
    //    var logger = sp.GetRequiredService<ILogger<Program>>();
    //    logger.LogError(message: "Failed to connect to InfluxDB: ");
    //    throw new InvalidOperationException("Failed to connect to InfluxDB.");
    //}
    //else
    //{
    //    var logger = sp.GetRequiredService<ILogger<Program>>();
    //    logger.LogInformation("Connected to InfluxDB successfully.");
    //}

    return new InfluxDBClient(options.Url, options.Token);
});

builder.Services.Configure<MqttConnectionConfiguration>
    (builder.Configuration.GetSection("MqttConnectionConfig"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MqttConnectionConfiguration>>().Value);
builder.Services.AddSingleton<IMqttService, MqttClientService>();

builder.Services.AddHostedService<Worker>();

IHost host = builder.Build();
host.Run();