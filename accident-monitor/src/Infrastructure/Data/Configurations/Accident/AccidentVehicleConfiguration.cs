using AccidentMonitoring.Domain.Entities.Accident;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentMonitoring.Infrastructure.Data.Configurations.Accident;
public class AccidentVehicleConfiguration : IEntityTypeConfiguration<AccidentVehicle>
{
    public void Configure(EntityTypeBuilder<AccidentVehicle> builder)
    {
        builder.HasKey(av => new { av.AccidentId, av.VehicleId });
        
        builder.Property(av => av.Id).ValueGeneratedOnAdd();

        builder.HasOne(av => av.Accident)
            .WithMany(a => a.AccidentVehicles)
            .HasForeignKey(av => av.AccidentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(av => av.Vehicle)
            .WithMany(v => v.AccidentVehicles)
            .HasForeignKey(av => av.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
