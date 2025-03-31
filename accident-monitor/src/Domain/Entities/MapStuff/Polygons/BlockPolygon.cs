using System.ComponentModel.DataAnnotations.Schema;
using AccidentMonitoring.Domain.Entities.Accident;

namespace AccidentMonitoring.Domain.Entities.MapStuff.Polygons
{
    public class BlockPolygon : BaseAuditableEntity
    {
        public int AccidentId { get; set; }
        public required virtual AccidentEntity Accident { get; set; }
        public virtual ICollection<PolygonCoordinate> Coordinates { get; set; } = [];
    }
}
