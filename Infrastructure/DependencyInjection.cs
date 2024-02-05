using Microsoft.Extensions.DependencyInjection;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IShipsRepository, ShipsRepository>();

        services.AddDbContext<ShipDbContext>(options => options.UseInMemoryDatabase(databaseName: "ShipDb"));

        return services;
    }
}
