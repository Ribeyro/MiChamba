using MyChamba.DTOs.Proyecto;

namespace MyChamba.Data.Repositories.Student;

public interface IStudentRepository
{
    Task<List<ProyectoCompletoDto>> ObtenerProyectosCompletosAsync();
    Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null);
}