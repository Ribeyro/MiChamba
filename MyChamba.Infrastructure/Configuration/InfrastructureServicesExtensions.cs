using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Data;
using MyChamba.Data.UnitofWork;
using MyChamba.Infrastructure.Data.Repositories;
using MyChamba.Repositories;

namespace MyChamba.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    // En MyChamba.Infrastructure.Configuration.InfrastructureServicesExtensions.cs
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<MyChambaContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );

        // Repositorios y UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IProyectoRepository, ProyectoRepository>();
        services.AddScoped<IPostulanteRepository, PostulanteRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ICertificadoRepository, CertificadoRepository>();

        return services;
    }

}