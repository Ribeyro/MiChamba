using Microsoft.OpenApi.Models;
using MyChamba.Application.Configuration;
using MyChamba.Infrastructure.Configuration;

namespace MyChamba.Configuration;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Infraestructura y repositorios
        services.AddInfrastructureServices(configuration);

        // Casos de uso de Application
        services.ApplicationServicesExtencion();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1",
                Description = "API para gestionar recursos."
            });
        });

        return services;
    }
}