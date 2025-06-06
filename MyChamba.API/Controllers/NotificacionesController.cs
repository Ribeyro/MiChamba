using Microsoft.AspNetCore.Mvc;
using MyChamba.Services.Interfaces;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificacionesController : ControllerBase
{
    private readonly INotificacionService _notificacionService;

    public NotificacionesController(INotificacionService notificacionService)
    {
        _notificacionService = notificacionService;
    }

    [HttpGet("empresa/{idEmpresa}")]
    public async Task<IActionResult> ObtenerPorEmpresa(ulong idEmpresa)
    {
        var resultado = await _notificacionService.ObtenerPorEmpresaAsync(idEmpresa);
        return Ok(resultado);
    }

    [HttpGet("estudiante/{idEstudiante}")]
    public async Task<IActionResult> ObtenerPorEstudiante(ulong idEstudiante)
    {
        var resultado = await _notificacionService.ObtenerPorEstudianteAsync(idEstudiante);
        return Ok(resultado);
    }
}