namespace AccidentMonitoring.Domain.Entities.MapStuff;
public class Coordinate
{
    public Coordinate()
    {
    }

    public Coordinate(Coordinate other) 
    {
        Latitude = other.Latitude;
        Longitude = other.Longitude;
    }
    public Coordinate(float longitude, float latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}
