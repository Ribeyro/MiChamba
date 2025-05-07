using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyChamba.Data;
using MyChamba.Data.Context;
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
builder.Services.AddControllers(); // Add support for JSON responses
builder.Services.AddSwaggerGen(); // Add Swagger for API testing


// Add DbContext with connection string from appsettings.json
builder.Services.AddDbContext<MyChambaContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 41))));

// Register UnitOfWork and Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<ISolicitudService, SolicitudService>(); //Patrick - nuevo
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IVerificacionService, VerificacionService>();


// Add password hasher for identity
builder.Services.AddScoped<IPasswordHasher<MyChamba.Models.Usuario>, PasswordHasher<MyChamba.Models.Usuario>>();
// Configuración JWT externalizada
builder.Services.AddJwtAuthentication(builder.Configuration);

//Habilitar CORS correctamente
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:19006") // Next.js, Expo
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication(); // ⬅️ Agrega esta línea antes de Authorization
app.UseAuthorization();

// Map API controllers
app.MapControllers(); // Enable attribute-based routing for API endpoints

// Enable Swagger for API testing
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.Run();