using Microsoft.Extensions.DependencyInjection;

namespace MyChamba.Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registra todos los Handlers que implementen IRequestHandler
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly)
        );
        // Aquí puedes agregar más servicios comunes de la capa Application si los necesitas
        
        return services;
    }
}