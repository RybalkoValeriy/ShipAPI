using Application.Abstractions.Exceptions;
using ShipApi.Models;
using System.Net;

namespace ShipApi.Infrastructure.Middleware;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(
        RequestDelegate next,
        ILogger<CustomExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var error = exception switch
        {
            ShipNotFoundException => new ErrorDetails((int)HttpStatusCode.NotFound, exception.Message),
            ArgumentException => new ErrorDetails((int)HttpStatusCode.BadRequest, exception.Message),
            UnresolvedDependencyException => new ErrorDetails((int)HttpStatusCode.InternalServerError, exception.Message),
            _ => new ErrorDetails((int)HttpStatusCode.InternalServerError, "Internal Server Error from the custom middleware.")
        };

        context.Response.StatusCode = error.StatusCode;

        await context.Response.WriteAsync(error.ToString());
    }
}