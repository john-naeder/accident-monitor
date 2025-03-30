namespace AccidentMonitoring.Domain.Entities.MapStuff.Polygons
{
    public class PolygonCoordinate : BaseAuditableEntity, IEquatable<PolygonCoordinate>
    {
        public int BlockPolygonId { get; set; }
        public Coordinate Coordinate { get; set; } = new Coordinate();
        public virtual BlockPolygon BlockPolygon { get; set; } = null!;

        public PolygonCoordinate() { }

        public PolygonCoordinate(int blockPolygonId, float longitude, float latitude)
        {
            BlockPolygonId = blockPolygonId;
            Coordinate = new Coordinate(longitude, latitude);
        }
        public bool Equals(PolygonCoordinate? other)
        {
            return other is not null && Coordinate.Longitude 
                == other.Coordinate.Longitude && Coordinate.Latitude 
                == other.Coordinate.Latitude;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Coordinate.Longitude, Coordinate.Latitude);
        }

        public override bool Equals(object? obj)
        {
            return ((IEquatable<PolygonCoordinate>)this).Equals(obj as PolygonCoordinate);
        }
    }
}
