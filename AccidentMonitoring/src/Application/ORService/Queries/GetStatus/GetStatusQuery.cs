﻿using AccidentMonitoring.Application.Common.Interfaces;
using AccidentMonitoring.Application.ServicesCheck.ORService;
namespace AccidentMonitoring.Application.ORService.Queries.GetStatus;

public record GetStatusQuery : IRequest<GetStatusORSResponseDto>;
public class GetStatusQueryHandler(IORService orServices) : IRequestHandler<GetStatusQuery, GetStatusORSResponseDto>
{
    private readonly IORService _orServices = orServices;

    public async Task<GetStatusORSResponseDto> Handle(
        GetStatusQuery request, CancellationToken cancellationToken)
    {
        return await _orServices.GetStatus<GetStatusORSResponseDto>();
    }
}
