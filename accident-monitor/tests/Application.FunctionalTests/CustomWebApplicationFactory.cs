using System.Data.Common;
using AccidentMonitoring.Application.Common.Interfaces;
using AccidentMonitoring.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace AccidentMonitoring.Application.FunctionalTests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly DbConnection _connection;
    private readonly string _connectionString;

    public CustomWebApplicationFactory(DbConnection connection, string connectionString)
    {
        _connection = connection;
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:AccidentMonitoringDb", _connectionString);
        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<IUser>();
                //.AddTransient(provider => Mock.Of<IUser>(s => s.Id == GetUserId()));
        });
    }
}
