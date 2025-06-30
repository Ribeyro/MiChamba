using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.DTOs.Certificados;
using MyChamba.Application.UseCases.Certificado.ObtenerCertificadoporEstudiante;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CertificadosController : ControllerBase
{
    private readonly IMediator _mediator;

    public CertificadosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{idEstudiante}")]
    public async Task<IActionResult> GetCertificados(ulong idEstudiante)
    {
        var result = await _mediator.Send(new GetCertificadosByEstudianteIdQuery(idEstudiante));
        return Ok(result);
    }
}