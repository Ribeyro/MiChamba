using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.UseCases.Recompensas.Commands;
using MyChamba.Application.UseCases.Recompensas.Queries;
using MyChamba.DTOs.Recompensa;
using MyChamba.Models;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecompensaController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecompensaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/recompensa/asignar
    [HttpPost("asignar")]
    public async Task<IActionResult> AsignarRecompensa([FromBody] RecompensaCreateDto dto)
    {
        var result = await _mediator.Send(new AsignarRecompensaCommand(dto));
        return Ok(result);
    }

    // POST: api/recompensa/economica
    [HttpPost("economica")]
    public async Task<IActionResult> AgregarRecompensaEconomica([FromBody] RecompensaEconomicaCreateDto dto)
    {
        var result = await _mediator.Send(new AgregarRecompensaEconomicaCommand(dto));
        return Ok(result);
    }

    // PUT: api/recompensa/economica/{id}
    [HttpPut("economica/{id}")]
    public async Task<IActionResult> ActualizarRecompensaEconomica(uint id, [FromBody] RecompensaEconomicaUpdateDto dto)
    {
        var result = await _mediator.Send(new ActualizarRecompensaEconomicaCommand(id, dto));
        return Ok(result);
    }

    // GET: api/recompensa
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllRecompensasQuery());
        return Ok(result);
    }

    // GET: api/recompensa/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(uint id)
    {
        var result = await _mediator.Send(new GetRecompensaByIdQuery(id));
        return Ok(result);
    }
}