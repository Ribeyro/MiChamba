using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.UseCases.Usuarios.Commands;
using MyChamba.DTOs.Register;


namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRegisterController ( IMediator _mediator ) : ControllerBase
{
    
    [HttpPost("register/empresa")]
    public async Task<IActionResult> RegisterEmpresa([FromBody] RegisterEmpresaRequest request)
    {
        var result = await _mediator.Send(new RegisterEmpresaCommand(request));
        return CreatedAtAction(nameof(RegisterEmpresa), new { id = result.Id }, result);
    }
    
    [HttpPost("register/estudiante")]
    public async Task<IActionResult> RegistrarEstudiante([FromBody] RegisterEstudianteRequest request)
    {
        var result = await _mediator.Send(new RegisterEstudianteCommand(request));
        return Ok(result);
    }
}