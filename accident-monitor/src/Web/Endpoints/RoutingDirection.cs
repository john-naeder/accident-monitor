using AccidentMonitoring.Application.ORService.Queries.GetDirections;
using AccidentMonitoring.Application.ORService.Queries.GetDirections.Dto;
using AccidentMonitoring.Application.ORService.Queries.GetDirections.Dtos;
using AccidentMonitoring.Application.ORService.Queries.GetStatus;
using AccidentMonitoring.Application.ORService.Queries.HealthCheck;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AccidentMonitoring.Web.Endpoints;
public class RoutingDirection : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this).RequireAuthorization();
        group.MapGet("direction/{profile}", GetDirection);
        group.MapGet("status", GetStatus);
        group.MapGet("health", HealthCheck);
    }

    private async Task<IResult> HealthCheck(ISender sender)
    { 
        var result = await sender.Send(new HealthCheckQuery());
        if (result.Status == "ready")
            return TypedResults.Ok(result);
        return TypedResults.InternalServerError(result);
    }

    public async Task<IResult> GetStatus(ISender sender)
    {
        var result = await sender.Send(new GetStatusQuery());

        return TypedResults.Ok(result);
    }

    public async Task<Ok<DirectionDefaultCutResponseDto>> GetDirection(
        ISender sender, [FromRoute] string profile, 
        [FromQuery] string start, [FromQuery] string end)
    {
        var requestDto = new GetDirectionDefaultRequestDto
        {
            StartingCoordinate = start,
            DestinationCoordinate = end
        };
        var query = new GetDirectionDefaultQuery(profile, requestDto);
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }
}
