using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.UseCases.Auth.Jwt;
using MyChamba.Application.UseCases.Auth.Login;
using MyChamba.Application.UseCases.Empresas.ObtenerDatosEmpresa;
using MyChamba.Application.UseCases.Estudiantes.ObtenerRetosDisponibles;
using MyChamba.Application.UseCases.Estudiantes.PerfilEstudiante;
using MyChamba.Application.UseCases.Notificaciones.CrearNotificacion;
using MyChamba.Domain.Models;
using MyChamba.Services.Implementations;

namespace MyChamba.Application.Configuration;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection ApplicationServicesExtencion(this IServiceCollection services)
    {
        // Registramos el hasher necesario para Login y Registro
        services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
        
        // Casos de uso (Application Layer)
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<ICrearNotificacionUseCase, CrearNotificacionUseCase>();
        services.AddScoped<IObtenerRetosDisponiblesUseCase, ObtenerRetosDisponiblesUseCase>();
        services.AddScoped<IEstudianteProfileUseCase, EstudianteProfileUseCase>();
        services.AddScoped<IEmpresaProfileUseCase, EmpresaProfileUseCase>();
        return services;
    }
}