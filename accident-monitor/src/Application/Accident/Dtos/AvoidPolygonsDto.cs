using AccidentMonitoring.Domain.Entities.MapStuff;
using AccidentMonitoring.Domain.Entities.MapStuff.Polygons;

namespace AccidentMonitoring.Application.Accident.Dtos;

public class AvoidPolygonsDto(BlockPolygon blockPolygon, string type)
{
    // TODO(): Refactor this to use a mapper
    public Coordinate[] Coordinates { get; set; } = blockPolygon.Coordinates.Select(c =>
            new Coordinate(c.Coordinate)).ToArray();
    public string Type { get; set; } = type;
}
