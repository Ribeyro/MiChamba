using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Implementations.Student;

public interface IStudentService
{
    Task<List<ProyectoCompletoDto>> ObtenerProyectosDisponiblesAsync();
    Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null);
}