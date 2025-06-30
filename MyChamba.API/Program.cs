using MyChamba.Application.Configuration;
using MyChamba.Configuration;
using MyChamba.Extensions;
using MyChamba.Middlewares;
using MyChamba.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 🔧 Agrega primero los servicios de infraestructura (repos, DB, UnitOfWork, etc.)
builder.Services.AddApplicationServices(); // Sin parámetros
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddProjectServices(builder.Configuration);
// 🔧 Luego los servicios de aplicación (casos de uso, JWT, etc.)

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

// Middleware pipeline
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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

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