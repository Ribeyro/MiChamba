using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Application.UseCases.Estudiantes.ObtenerRetosDisponibles;

public interface IObtenerRetosDisponiblesUseCase
{
    Task<List<ProyectoCompletoDto>> ObtenerProyectosDisponiblesAsync();
    Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null);
}

public class ObtenerRetosDisponiblesUseCase(IStudentRepository studentRepository) : IObtenerRetosDisponiblesUseCase
{
    public async Task<List<ProyectoCompletoDto>> ObtenerProyectosDisponiblesAsync()
    {
        return await studentRepository.ObtenerProyectosCompletosAsync();
    }

    public async Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto,
        uint? idEmpresa = null)
    {
        return await studentRepository.ObtenerProyectosFiltradosAsync(fechaTexto, idEmpresa);
    }
}