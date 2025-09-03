using Microsoft.AspNetCore.SignalR;

namespace ProjectManagement.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSignalR();

        return services;
    }
}
