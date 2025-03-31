using AccidentMonitoring.Application.ORService.Queries.GetDirections;

namespace AccidentMonitoring.Application.Common.Interfaces;
public interface IORService : IExternalServices
{
    Task<TResponse> GetDefaultRoutingDirectionAsync<TResponse>(
        string profile, GetDirectionDefaultRequestDto request);
}
