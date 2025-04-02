using AccidentMonitoring.Domain.Entities.MapStuff.Polygons;

namespace AccidentMonitoring.Domain.Entities.Accident
{
    public class AccidentEntity(
        DateTime timestamp,
        float longitude,
        float latitude,
        AccidentSeverity severity = AccidentSeverity.Low,
        bool isBlockingWay = false,
        AccidentResolvedStatus resolvedStatus = AccidentResolvedStatus.Unresolved) : BaseAuditableEntity
    {
        public DateTime Timestamp { get; set; } = timestamp;
        public float Longitude { get; set; } = longitude;
        public float Latitude { get; set; } = latitude;
        public AccidentSeverity Severity { get; set; } = severity;
        public bool IsBlockingWay { get; set; } = isBlockingWay;
        public AccidentResolvedStatus ResolvedStatus { get; set; } = resolvedStatus;
        public virtual ICollection<AccidentInvolved> AccidentInvolved { get; set; } = [];
        public virtual BlockPolygon BlockPolygon { get; set; } = null!;
    }
}
