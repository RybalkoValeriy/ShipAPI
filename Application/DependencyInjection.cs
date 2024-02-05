using Application.Abstractions.Commands;
using Application.Abstractions.Queries;
using Application.Concrete.Commands;
using Application.Concrete.Queries;
using Application.Ships;
using Application.Ships.Create;
using Application.Ships.Delete;
using Application.Ships.Get;
using Application.Ships.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IQueryResolver), typeof(QueryResolver));
        services.AddScoped(typeof(ICommandResolver), typeof(CommandResolver));

        services.AddScoped<ICommandHandler<CreateShipCommand>, CreateShipCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteShipCommand>, DeleteShipCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateShipCommand>, UpdateShipCommandHandler>();

        services.AddScoped<IQueryHandler<GetAllShipsQuery, IEnumerable<ShipResponse>>, GetAllShipsQueryHandler>();
        services.AddScoped<IQueryHandler<GetShipByIdQuery, ShipResponse>, GetShipByIdQueryHandler>();

        return services;
    }
}
