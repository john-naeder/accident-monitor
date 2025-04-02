using AccidentMonitoring.Domain.Entities.Accident;
using AccidentMonitoring.Domain.Entities.MapStuff.Polygons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentMonitoring.Infrastructure.Data.Configurations.Accident;
public class AccidentEntityConfiguration : IEntityTypeConfiguration<AccidentEntity>
{
    public void Configure(EntityTypeBuilder<AccidentEntity> builder)
    {
        builder.Property(v => v.Id)
            .HasColumnName("Guid")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Timestamp).IsRequired();
        builder.Property(a => a.Longitude).IsRequired();
        builder.Property(a => a.Latitude).IsRequired();

        builder.HasMany(a => a.AccidentInvolved)
            .WithOne(av => av.Accident)
            .HasForeignKey(av => av.AccidentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.BlockPolygon)
            .WithOne(bp => bp.Accident)
            .HasForeignKey<BlockPolygon>(bp => bp.AccidentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
