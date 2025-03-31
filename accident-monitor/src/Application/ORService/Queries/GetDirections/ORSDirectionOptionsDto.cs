using AccidentMonitoring.Application.AvoidPolygons.Dtos;

namespace AccidentMonitoring.Application.ORService.Queries.GetDirections;

public class ORSDirectionOptionsDto
{
    public AvoidPolygonsDto? AvoidPolygons { get; set; }
    public string[]? AvoidFeatures { get; set; } = [];
    public int[]? AvoidContries { get; set; } = [];
}
