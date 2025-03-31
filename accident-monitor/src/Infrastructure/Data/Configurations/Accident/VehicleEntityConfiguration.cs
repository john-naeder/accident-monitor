using AccidentMonitoring.Domain.Entities.Accident;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentMonitoring.Infrastructure.Data.Configurations.Accident;
public class VehicleEntityConfiguration : IEntityTypeConfiguration<VehicleEntity>
{
    public void Configure(EntityTypeBuilder<VehicleEntity> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(v => v.LicensePlate).IsUnique();
        builder.Property(v => v.LicensePlate)
            .HasMaxLength(10)
            .IsRequired();


        builder.HasMany(v => v.AccidentVehicles) 
            .WithOne(av => av.Vehicle)
            .HasForeignKey(av => av.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
