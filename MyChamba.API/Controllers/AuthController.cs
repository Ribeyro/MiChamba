using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.UseCases.Auth.Login;
using MyChamba.DTOs.Auth;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginUseCase _authService;

    // 🔧 Constructor necesario para que ASP.NET Core pueda inyectar el servicio
    public AuthController(ILoginUseCase authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en login: {ex.Message}");
        }
    }
}