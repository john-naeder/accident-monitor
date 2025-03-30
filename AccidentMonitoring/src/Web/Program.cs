using AccidentMonitoring.Infrastructure;
using AccidentMonitoring.Infrastructure.Data;
using AccidentMonitoring.Infrastructure.MQTT;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.AddServiceDefaults();
        builder.AddKeyVaultIfConfigured();
        builder.AddApplicationServices();
        builder.AddInfrastructureServices();
        builder.AddWebServices();
        builder.Services.Configure<MqttConnectionConfiguration>(
            builder.Configuration.GetSection("MqttConnectionConfig"));
        var app = builder.Build();

        await app.InitializeMqttAsync();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            await app.InitializeDatabaseAsync();
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseSwaggerUi(settings =>
        {
            settings.Path = "/api";
            settings.DocumentPath = "/api/specification.json";
        });

        app.UseExceptionHandler(options => { });

        app.Map("/", () => Results.Redirect("/api"));

        app.MapDefaultEndpoints();
        app.MapEndpoints();

        await app.RunAsync();
    }
}