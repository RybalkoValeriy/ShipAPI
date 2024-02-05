using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Integration.Tests;

public class ShipApiApplicationFactory : WebApplicationFactory<Program>
{
    private const string Database = "master";
    private const string Username = "sa";
    private const string Password = "passw0rd";
    private const ushort MsSqlPort = 1433;

    private readonly IContainer _msSqlContainer;

    public ShipApiApplicationFactory()
    {
        _msSqlContainer = new ContainerBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPortBinding(MsSqlPort)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("SQLCMDUSER", Username)
            .WithEnvironment("SQLCMDPASSWORD", Password)
            .WithEnvironment("MSSQL_SA_PASSWORD", Password)
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var host = _msSqlContainer.Hostname;
        var port = _msSqlContainer.GetMappedPublicPort(MsSqlPort);

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ShipDbContext>));

            services.AddDbContext<ShipDbContext>(
                options =>
                options.UseSqlServer(
                    $"Server={host},{port};Database={Database};User Id={Username};Password={Password};TrustServerCertificate=True"));
        });
    }

    public Task InitializeAsync() => _msSqlContainer.StartAsync();

    public new ValueTask DisposeAsync() => _msSqlContainer.DisposeAsync();
}
