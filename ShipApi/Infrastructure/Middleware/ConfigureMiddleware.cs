namespace ShipApi.Infrastructure.Middleware;

public static class ConfigureMiddleware
{
    public static void ConfigureExceptionMiddleware(this WebApplication app) =>
        app.UseMiddleware<CustomExceptionMiddleware>();
}


