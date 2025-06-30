using Microsoft.Extensions.Logging;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Empresas;

namespace MyChamba.Application.UseCases.Empresas.ObtenerDatosEmpresa;


public interface IEmpresaProfileUseCase
{
    Task<EmpresaProfileDto> GetProfileAsync(ulong idUsuario);
}

public class EmpresaProfileUseCase : IEmpresaProfileUseCase
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly ILogger<EmpresaProfileUseCase> _logger;

    public EmpresaProfileUseCase(IEmpresaRepository empresaRepository, ILogger<EmpresaProfileUseCase> logger)
    {
        _empresaRepository = empresaRepository;
        _logger = logger;
    }

    public async Task<EmpresaProfileDto> GetProfileAsync(ulong idUsuario)
    {
        try
        {
            var profile = await _empresaRepository.GetProfileAsync(idUsuario);

            if (profile == null)
            {
                _logger.LogWarning($"Perfil de empresa no encontrado para el usuario {idUsuario}");
                throw new KeyNotFoundException("Perfil de empresa no encontrado");
            }

            return profile;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al obtener perfil de empresa para el usuario {idUsuario}");
            throw;
        }
    }
}