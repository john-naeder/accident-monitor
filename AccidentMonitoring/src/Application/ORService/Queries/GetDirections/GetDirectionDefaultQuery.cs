using AccidentMonitoring.Application.Common.Interfaces;

namespace AccidentMonitoring.Application.ORService.Queries.GetDirections
{
    public record GetDirectionDefaultQuery(string Profile, GetDirectionDefaultRequestDto RequestDto)
        : IRequest<GetDirectionDefaultResponseDto>;

    public class GetDirectionDefaultQueryHandler(IORService orServices) 
        : IRequestHandler<GetDirectionDefaultQuery, GetDirectionDefaultResponseDto>
    {
        private readonly IORService _orServices = orServices;

        public async Task<GetDirectionDefaultResponseDto> 
            Handle(GetDirectionDefaultQuery request, CancellationToken cancellationToken)
        {
            return await _orServices.GetDefaultRoutingDirectionAsync<GetDirectionDefaultResponseDto>(
                request.Profile, request.RequestDto);
        }
    }
}
