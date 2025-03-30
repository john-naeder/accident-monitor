using AccidentMonitoring.Domain.Entities.MapStuff.Polygons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentMonitoring.Infrastructure.Data.Configurations.MapStuff;
public class BlockPolygonConfiguration : IEntityTypeConfiguration<BlockPolygon>
{
    public void Configure(EntityTypeBuilder<BlockPolygon> builder)
    {
        builder.Property(bp => bp.Id)
            .ValueGeneratedOnAdd();
        builder.HasKey(bp => bp.Id);
        builder.HasOne(bp => bp.Accident)
            .WithOne(a => a.BlockPolygon)
            .HasForeignKey<BlockPolygon>(bp => bp.AccidentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(bp => bp.Coordinates)
            .WithOne(pc => pc.BlockPolygon)
            .HasForeignKey(pc => pc.BlockPolygonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
