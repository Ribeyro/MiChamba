using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyChamba.Configuration;
using MyChamba.Data;
using MyChamba.Data.Repositories;
using MyChamba.Data.Repositories.Student;
using MyChamba.Data.Interface;
using MyChamba.Data.UnitofWork;
using MyChamba.Domain.Interface;
using MyChamba.Extensions;
using MyChamba.Services.Implementations;
using MyChamba.Services.Interfaces;
using MyChamba.Helpers;
using MyChamba.Infrastructure.Data.Repositories;
using MyChamba.Middlewares;
using MyChamba.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Centralización de configuración (infraestructura y más)
builder.Services.AddApplicationServices(builder.Configuration);


// Register UnitOfWork and Services

builder.Services.AddJwtAuthentication(builder.Configuration);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:19006", "https://fullchamba.netlify.app")
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
