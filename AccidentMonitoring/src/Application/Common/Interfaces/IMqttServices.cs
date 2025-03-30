using AccidentMonitoring.Application.Common.Results;

namespace AccidentMonitoring.Application.Common.Interfaces;

public interface IMqttServices : IExternalServices
{
    Task<ServiceResult> StartAsync();
    Task<ServiceResult> ConnectAsync();
    ValueTask DisposeAsync();
    Task<ServiceResult> PublishAsync<T>(string topic, T dto);
    Task<ServiceResult> SubscribeAsync();
}
