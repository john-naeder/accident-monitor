using AccidentMonitoring.Domain.Entities.MapStuff.Polygons;

namespace AccidentMonitoring.Domain.Entities.Accident
{
    public class AccidentEntity : BaseAuditableEntity
    {
        public AccidentEntity(
            DateTime timestamp,
            float longitude,
            float latitude,
            AccidentSeverity severity = AccidentSeverity.Low,
            bool isBlockingWay = false,
            AccidentResolvedStatus resolvedStatus = AccidentResolvedStatus.Unresolved)
        {
            Timestamp = timestamp;
            Longitude = longitude;
            Latitude = latitude;
            Severity = severity;
            ResolvedStatus = resolvedStatus;
            IsBlockingWay = isBlockingWay;
        }
        public DateTime Timestamp { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public AccidentSeverity Severity { get; set; } = AccidentSeverity.Low;
        public bool IsBlockingWay { get; set; } = false;
        public AccidentResolvedStatus ResolvedStatus { get; set; } = AccidentResolvedStatus.Unresolved;
        public virtual ICollection<AccidentVehicle> AccidentVehicles { get; set; } = [];
        public virtual BlockPolygon? BlockPolygon { get; set; }
    }
}
