using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyChamba.Application.UseCases.Auth.Jwt;
using MyChamba.Application.UseCases.Auth.Login;
using MyChamba.Application.UseCases.Estudiantes.ObtenerRetosDisponibles;
using MyChamba.Application.UseCases.Estudiantes.PerfilEstudiante;
using MyChamba.Application.UseCases.Notificaciones.CrearNotificacion;
using MyChamba.Application.UseCases.Postulaciones.AceptarPostulante;
using MyChamba.Application.UseCases.Postulaciones.ObtenerPostulantes;
using MyChamba.Application.UseCases.Postulaciones.PostularEstudiante;
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
        services.AddScoped<IListarProyectosPorEmpresaUseCase, ListarRetosDisponiblesUseCase>();
        services.AddScoped<ICrearNotificacionUseCase, CrearNotificacionUseCase>();
        services.AddScoped<IAceptarPostulanteUseCase, AceptarPostulanteUseCase>();
        services.AddScoped<IObtenerPostulantesPorProyectoUseCase, ObtenerPostulantesPorProyectoUseCase>();
        services.AddScoped<IPostularEstudianteUseCase, PostularEstudianteUseCase>();
        services.AddScoped<IObtenerRetosDisponiblesUseCase, ObtenerRetosDisponiblesUseCase>();
        services.AddScoped<IEstudianteProfileUseCase, EstudianteProfileUseCase>();
        return services;
    }
}