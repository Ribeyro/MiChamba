using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyChamba.Data;
using MyChamba.Data.Repositories;
using MyChamba.Data.Repositories.Student;
using MyChamba.Data.Interface;
using MyChamba.Data.UnitofWork;
using MyChamba.Extensions;
using MyChamba.Services.Implementations;
using MyChamba.Services.Interfaces;
using MyChamba.Helpers;
using MyChamba.Middlewares;
using MyChamba.Repositories;
using MyChamba.Services.Implementations.Student;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// ✅ Add DbContext with retry-on-failure for transient Railway issues
builder.Services.AddDbContext<MyChambaContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 41)),
        mysqlOptions => mysqlOptions.EnableRetryOnFailure()
    )
);

// Register UnitOfWork and Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<ISolicitudService, SolicitudService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<IPostulanteRepository, PostulanteRepository>();
builder.Services.AddScoped<IPostulanteService, PostulanteService>();

builder.Services.AddScoped<IPasswordHasher<MyChamba.Models.Usuario>, PasswordHasher<MyChamba.Models.Usuario>>();
builder.Services.AddJwtAuthentication(builder.Configuration);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:19006")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Global error logging middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ EXCEPCIÓN NO CONTROLADA: {ex.Message}");
        throw;
    }
});

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.Run();
