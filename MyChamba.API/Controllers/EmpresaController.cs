using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.DTOs.Empresas;
using MyChamba.Application.UseCases.Empresas.ObtenerDatosEmpresa;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase
{
    private readonly IEmpresaProfileUseCase _empresaProfileUseCase;

    public EmpresaController(IEmpresaProfileUseCase empresaProfileUseCase)
    {
        _empresaProfileUseCase = empresaProfileUseCase;
    }

    [HttpGet("{idUsuario}")]
    public async Task<ActionResult<EmpresaProfileDto>> GetProfile(ulong idUsuario)
    {
        return await _empresaProfileUseCase.GetProfileAsync(idUsuario);
    }
}