using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccidentMonitoring.Domain.Entities.Accident
{
    public class AccidentVehicle : BaseAuditableEntity
    {
        public int AccidentId { get; set; }
        public int VehicleId { get; set; }
        public virtual required AccidentEntity Accident { get; set; }
        public virtual required VehicleEntity Vehicle { get; set; }
    }
}
