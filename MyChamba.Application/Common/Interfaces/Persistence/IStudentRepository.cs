using MyChamba.Application.DTOs.Student;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Application.Common.Interfaces.Persistence;

public interface IStudentRepository
{
    Task<List<ProyectoCompletoDto>> ObtenerProyectosCompletosAsync();
    Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null);
    Task<EstudianteProfileDto> GetProfileAsync(ulong idUsuario);
    Task UpdateProfileAsync(ulong idUsuario, UpdateEstudianteProfileDto profileDto);
}