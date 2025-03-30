using System.Reflection;
using AccidentMonitoring.Application.Common.Interfaces;
using AccidentMonitoring.Domain.Entities.Accident;
using AccidentMonitoring.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccidentMonitoring.Infrastructure.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<AccidentEntity> Accidents => Set<AccidentEntity>();

    public DbSet<AccidentDetails> AccidentDetails => Set<AccidentDetails>();

    public DbSet<VehicleEntity> Vehicles => Set<VehicleEntity>();

    public DbSet<AccidentVehicle> AccidentVehicles => Set<AccidentVehicle>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
