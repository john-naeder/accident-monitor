using AccidentMonitoring.Application.Common.Interfaces;
using AccidentMonitoring.Application.DTOs;

namespace AccidentMonitoring.Application.ORService.Queries.GetDirections
{
    public record GetDirectionDefaultQuery(string Profile, GetDirectionDefaultRequestDto RequestDto)
        : IRequest<DirectionDefaultCutResponseDto>;

    public class GetDirectionDefaultQueryHandler(IORService orServices) 
        : IRequestHandler<GetDirectionDefaultQuery, DirectionDefaultCutResponseDto>
    {
        private readonly IORService _orServices = orServices;

        public async Task<DirectionDefaultCutResponseDto> 
            Handle(GetDirectionDefaultQuery request, CancellationToken cancellationToken)
        {
            var response =  await _orServices.GetDefaultRoutingDirectionAsync<GetDirectionDefaultResponseDto>(
                request.Profile, request.RequestDto);
            return DirectionMapper.Map(response);
        }
    }
}
