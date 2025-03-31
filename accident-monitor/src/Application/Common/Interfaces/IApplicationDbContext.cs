using AccidentMonitoring.Domain.Entities.Accident;

namespace AccidentMonitoring.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<AccidentEntity> Accidents { get; }

    DbSet<AccidentDetails> AccidentDetails { get; }

    DbSet<VehicleEntity> Vehicles { get; }

    DbSet<AccidentVehicle> AccidentVehicles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
