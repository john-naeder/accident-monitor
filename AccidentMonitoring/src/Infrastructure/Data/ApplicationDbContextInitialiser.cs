using AccidentMonitoring.Domain.Constants;
using AccidentMonitoring.Domain.Entities.Accident;
using AccidentMonitoring.Domain.Entities.MapStuff.Polygons;
using AccidentMonitoring.Domain.Enums;
using AccidentMonitoring.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AccidentMonitoring.Infrastructure.Data;
public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initialiser.InitializeAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
{
    public async Task InitializeAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
        }

        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await userManager.AddToRolesAsync(administrator, [administratorRole.Name]);
            }
        }
        if (!context.Vehicles.Any())
        {
            context.Vehicles.Add(new VehicleEntity
            {
                LicensePlate = "29A-12345",
            });
            context.Vehicles.Add(new VehicleEntity
            {
                LicensePlate = "30A-12345",
            });
            await context.SaveChangesAsync();
        }

        if (!context.Accidents.Any())
        {

            var accident = new AccidentEntity(
                DateTime.Now,
                106.6564777493477F,
                10.801198744414963F,
                AccidentSeverity.Medium,
                true,
                AccidentResolvedStatus.Unresolved
            );

            if (accident.IsBlockingWay)
            {
                var blockPolygon = new BlockPolygon
                {
                    Accident = accident,
                    Coordinates = []
                };

                var blockPolygonCoordinates = new List<PolygonCoordinate> {
                    new (
                        blockPolygon.Id,
                        106.6564777493477F,
                        10.801198744414963F
                    ),
                    new (
                        blockPolygon.Id,
                        106.6569777493477F,
                        10.801198744414963F
                    ),
                    new (
                        blockPolygon.Id,
                        106.6569777493477F,
                        10.801698744414963F
                    ),
                    new (
                        blockPolygon.Id,
                        106.6564777493477F,
                        10.801698744414963F
                    ),
                    new (
                        blockPolygon.Id,
                        106.6564777493477F,
                        10.801198744414963F
                    )
                };
                blockPolygon.Coordinates = blockPolygonCoordinates;
                accident.BlockPolygon = blockPolygon;
            }

            context.Accidents.Add(accident);

            var vehicle1 = context.Vehicles.FirstOrDefault(v => v.Id == 1);
            var vehicle2 = context.Vehicles.FirstOrDefault(v => v.Id == 2);

            if (vehicle1 != null)
            {
                accident.AccidentVehicles.Add(new AccidentVehicle
                {
                    Accident = accident,
                    Vehicle = vehicle1,
                });
            }
            if (vehicle2 != null)
            {
                accident.AccidentVehicles.Add(new AccidentVehicle
                {
                    Accident = accident,
                    Vehicle = vehicle2,
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
