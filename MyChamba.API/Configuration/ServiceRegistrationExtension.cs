using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using MyChamba.Application.Services.Implementations;
using MyChamba.Application.Services.Interfaces;
using MyChamba.Data.Interface;
using MyChamba.Data.Repositories;
using MyChamba.Data.Repositories.Student;
using MyChamba.Data.UnitofWork;
using MyChamba.Domain.Interface;
using MyChamba.Domain.Models;
using MyChamba.Helpers;
using MyChamba.Infrastructure.Configuration;
using MyChamba.Infrastructure.Data.Repositories;
using MyChamba.Repositories;
using MyChamba.Services.Implementations;
using MyChamba.Services.Interfaces;

namespace MyChamba.Configuration
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Llamamos a la configuración de infraestructura (base de datos, etc.)
            services.AddInfrastructureServices(configuration);
            
            
            // Register UnitOfWork and Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProyectoRepository, ProyectoRepository>();
            services.AddScoped<IProyectoService, ProyectoService>();
            services.AddScoped<ISolicitudService, SolicitudService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<INotificacionService, NotificacionService>();
            services.AddScoped<IPostulanteRepository, PostulanteRepository>();
            services.AddScoped<IPostulanteService, PostulanteService>();

            services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
            
            
            
            // Habilitar Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mi API",
                    Version = "v1",
                    Description = "API para gestionar recursos."
                });
            });

            return services;
        }
    }
}