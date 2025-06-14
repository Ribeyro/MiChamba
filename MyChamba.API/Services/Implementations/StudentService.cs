using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.DTOs.Proyecto;
using MyChamba.Services.Interfaces;

namespace MyChamba.Application.Services.Implementations;

public class StudentService (IStudentRepository _studentRepository) : IStudentService
{
    

    public async Task<List<ProyectoCompletoDto>> ObtenerProyectosDisponiblesAsync()
    {
        return await _studentRepository.ObtenerProyectosCompletosAsync();
    }

    public async Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null)
    {
        return await _studentRepository.ObtenerProyectosFiltradosAsync(fechaTexto, idEmpresa);
    }
}