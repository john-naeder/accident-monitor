using AccidentMonitoring.Application.Accident.Dtos;

namespace AccidentMonitoring.Application.ORService.Queries.GetDirections.Dto;

public class ORSDirectionOptionsDto
{
    public AvoidPolygonsDto? AvoidPolygons { get; set; }
    public string[]? AvoidFeatures { get; set; } = [];
    public int[]? AvoidCountries { get; set; } = [];
}
