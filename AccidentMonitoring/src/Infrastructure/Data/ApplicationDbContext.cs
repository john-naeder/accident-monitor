using System.Reflection;
using AccidentMonitoring.Application.Common.Interfaces;
using AccidentMonitoring.Domain.Entities.Accident;
using AccidentMonitoring.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

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
//// TODO: Refactor these to make it least access privileges to the database.
//public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//{
//    public ApplicationDbContext CreateDbContext(string[] args)
//    {
//        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "Web");
//        Console.WriteLine($"Base path: {basePath}");

//        var configuration = new ConfigurationBuilder()
//            .SetBasePath(basePath)
//            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//            .Build();

//        var migrationConnectionString = configuration.GetConnectionString("AccidentMonitorDB");
//        if (string.IsNullOrWhiteSpace(migrationConnectionString))
//        {
//            throw new InvalidOperationException("Connection string for migration actor not found.");
//        }

//        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//        optionsBuilder.UseSqlServer(migrationConnectionString);

//        return new ApplicationDbContext(optionsBuilder.Options);
//    }
//}
