using System;
using AccidentMonitoring.Application.Common.Interfaces;
using AccidentMonitoring.Domain.Constants;
using AccidentMonitoring.Infrastructure.Data;
using AccidentMonitoring.Infrastructure.Data.Interceptors;
using AccidentMonitoring.Infrastructure.Identity;
using AccidentMonitoring.Infrastructure.MQTT;
using AccidentMonitoring.Infrastructure.ORS;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AccidentMonitoring.Infrastructure;
public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var dbConnectionString = builder.Configuration.GetConnectionString("AccidentMonitorDB");
        Guard.Against.Null(dbConnectionString, message: "Connection string 'AccidentMonitorDB' not found.");

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(dbConnectionString);
            options.ConfigureWarnings(warnings =>
               warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        });

        builder.EnrichSqlServerDbContext<ApplicationDbContext>();

        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<ApplicationDbContextInitializer>();

        builder.Services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services.AddAuthorizationBuilder();

        builder.Services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddTransient<IIdentityService, IdentityService>();

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator));

        builder.Services.Configure<MqttConnectionConfiguration>(builder.Configuration.GetSection("MqttConnectionConfig"));
        builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MqttConnectionConfiguration>>().Value);
        builder.Services.AddSingleton<IMqttServices, MqttClientService>();
        builder.Services.AddScoped<MqttClientContextInitializer>();

        // ORService 
        builder.Services.AddSingleton<IORService, ORService>();
        var orsConnectionString = builder.Configuration.GetSection("ORSUri");
        builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection("ORS"));
        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<ApiConfiguration>>().Value);
    }
}
