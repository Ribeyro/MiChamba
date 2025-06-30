using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyChamba.Application.Behaviors;

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
        
        // FluentValidation (Registra todos los validators del assembly)
        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

        // Pipeline para validación
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}