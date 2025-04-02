namespace AccidentMonitoring.Domain.Entities.Accident
{
    public class VehicleEntity : BaseAuditableEntity
    {
        public VehicleEntity() { }
        public VehicleEntity(string registrationCertificateNumber, string licensePlate, string engineNumber,
                             string chassisNumber, string type, string brand, string model, string vehicleOwnerName,
                             string address, CitizenEntity owner)
        {
            RegistrationCertificateNumber = registrationCertificateNumber;
            LicensePlate = licensePlate;
            EngineNumber = engineNumber;
            ChassisNumber = chassisNumber;
            Type = type;
            Brand = brand;
            Model = model;
            VehicleOwnerName = vehicleOwnerName;
            Address = address;
            Owner = owner;
        }

        public string? RegistrationCertificateNumber { get; set; }
        public string? LicensePlate { get; set; }
        public string? EngineNumber { get; set; }
        public string? ChassisNumber { get; set; }
        public string? Type { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? VehicleOwnerName { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<AccidentInvolved> AccidentInvolved { get; set; } = [];
        public virtual CitizenEntity Owner { get; set; } = null!;
    }
}
