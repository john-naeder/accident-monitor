using AccidentMonitoring.Domain.Entities.Accident;

namespace AccidentMonitoring.Domain.Entities.MapStuff.Polygons
{
    public class PolygonCoordinate : BaseAuditableEntity, IEquatable<PolygonCoordinate>
    {
        public PolygonCoordinate() { }

        public PolygonCoordinate(float longitude, float latitude)
        {
            Coordinate = new Coordinate(longitude, latitude);
        }

        public Guid AccidentId { get; set; }
        public virtual AccidentEntity Accident { get; set; } = null!;

        public Coordinate Coordinate { get; set; } = new Coordinate();

        public bool Equals(PolygonCoordinate? other)
        {
            return other is not null
                && Coordinate.Longitude == other.Coordinate.Longitude
                && Coordinate.Latitude == other.Coordinate.Latitude;
        }

        public override int GetHashCode()
            => HashCode.Combine(Coordinate.Longitude, Coordinate.Latitude);

        public override bool Equals(object? obj)
            => Equals(obj as PolygonCoordinate);
    }
}
