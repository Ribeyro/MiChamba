using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyChamba.Data;
using MyChamba.Data.UnitofWork;
using MyChamba.Services.Implementations;
using MyChamba.Services.Interfaces;

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
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Add password hasher for identity
builder.Services.AddScoped<IPasswordHasher<MyChamba.Models.Usuario>, PasswordHasher<MyChamba.Models.Usuario>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

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