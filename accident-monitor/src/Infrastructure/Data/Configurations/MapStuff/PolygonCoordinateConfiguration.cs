using AccidentMonitoring.Domain.Entities.MapStuff.Polygons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentMonitoring.Infrastructure.Data.Configurations.MapStuff
{
    public class PolygonCoordinateConfiguration : IEntityTypeConfiguration<PolygonCoordinate>
    {
        public void Configure(EntityTypeBuilder<PolygonCoordinate> builder)
        {
            builder.Property(v => v.Id)
                .HasColumnName("Guid")
                .ValueGeneratedOnAdd();

            builder.HasOne(pc => pc.BlockPolygon)
                .WithMany(bp => bp.Coordinates)
                .HasForeignKey(pc => pc.BlockPolygonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(pc => pc.Coordinate, coordinateBuilder =>
            {
                coordinateBuilder.Property(c => c.Longitude)
                    .HasColumnName("Longitude")
                    .IsRequired();

                coordinateBuilder.Property(c => c.Latitude)
                    .HasColumnName("Latitude")
                    .IsRequired();
            });

            builder.Property(pc => pc.BlockPolygonId).IsRequired();
        }
    }
}
