using System.Collections;
using AccidentMonitor.Application.common;
using iot_monitor.Application.Enum;

namespace iot_monitor.Application.Interfaces;

public interface IMqttService
{
    public event Action<string, string>? OnMessageReceived;

    event Func<string, string, Task>? OnMessageReceivedAsync;
    Task<ServiceResult> StartAsync(CancellationToken cancellationToken = default);
    Task<ServiceResult> ConnectAsync(CancellationToken cancellationToken = default);
    Task DisconnectAsync(CancellationToken cancellationToken = default);
    ValueTask DisposeAsync();
    Task<ServiceResult> PublishAsync<T>(
            string topic,
            T payloadDto,
            MQTTQoSLevel mqttQoSLevel = MQTTQoSLevel.AtLeastOnce,
            bool retain = false,
            CancellationToken cancellationToken = default);
    Task<ServiceResult> SubscribeToConfiguredTopicsAsync(CancellationToken cancellationToken = default);
}
