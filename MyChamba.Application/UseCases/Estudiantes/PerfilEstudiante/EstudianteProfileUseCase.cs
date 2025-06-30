using Microsoft.Extensions.Logging;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Student;

namespace MyChamba.Application.UseCases.Estudiantes.PerfilEstudiante;

// Interfaz del caso de uso
public interface IEstudianteProfileUseCase
{
    Task<EstudianteProfileDto> GetProfileAsync(ulong idUsuario);
    Task UpdateProfileAsync(ulong idUsuario, UpdateEstudianteProfileDto profileDto);
}

// Implementación del caso de uso
public class EstudianteProfileUseCase : IEstudianteProfileUseCase
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILogger<EstudianteProfileUseCase> _logger;

    public EstudianteProfileUseCase(
        IStudentRepository studentRepository,
        ILogger<EstudianteProfileUseCase> logger)
    {
        _studentRepository = studentRepository;
        _logger = logger;
    }

    public async Task<EstudianteProfileDto> GetProfileAsync(ulong idUsuario)
    {
        try
        {
            var profile = await _studentRepository.GetProfileAsync(idUsuario);
            
            if (profile == null)
            {
                _logger.LogWarning($"Perfil no encontrado para el usuario {idUsuario}");
                throw new KeyNotFoundException("Perfil no encontrado");
            }
            
            return profile;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al obtener perfil para usuario {idUsuario}");
            throw;
        }
    }

    public async Task UpdateProfileAsync(ulong idUsuario, UpdateEstudianteProfileDto profileDto)
    {
        try
        {
            await _studentRepository.UpdateProfileAsync(idUsuario, profileDto);
            _logger.LogInformation($"Perfil actualizado para usuario {idUsuario}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al actualizar perfil para usuario {idUsuario}");
            throw;
        }
    }
}