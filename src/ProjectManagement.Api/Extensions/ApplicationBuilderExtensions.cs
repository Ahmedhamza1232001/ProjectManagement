using ProjectManagement.Api.Middleware;
using ProjectManagement.Api.SignalR;

namespace ProjectManagement.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApiMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }

    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();
        endpoints.MapHub<TaskHub>("/taskhub");
        return endpoints;
    }
}
