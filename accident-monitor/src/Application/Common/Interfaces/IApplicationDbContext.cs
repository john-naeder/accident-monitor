using AccidentMonitoring.Domain.Entities.Accident;
using AccidentMonitoring.Domain.Entities.MapStuff.Polygons;

namespace AccidentMonitoring.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<CitizenEntity> Citizens { get; }
    DbSet<VehicleEntity> Vehicles { get; }
    DbSet<AccidentEntity> Accidents { get; }
    DbSet<AccidentDetails> AccidentDetails { get; }
    DbSet<AccidentInvolved> AccidentInvolved { get; }
    DbSet<PolygonCoordinate> PolygonCoordinates { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
