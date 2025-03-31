using System.ComponentModel.DataAnnotations;

namespace AccidentMonitoring.Domain.Entities.Accident
{
    public class VehicleEntity : BaseAuditableEntity
    {
        public string? LicensePlate { get; set; }
        public virtual ICollection<AccidentVehicle> AccidentVehicles { get; set; } = [];
    }
}
