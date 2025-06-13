using MyChamba.Domain.Entities;

namespace MyChamba.Domain.Interface;

public interface IStudentRepository
{
    Task<List<ProyectoCompletoDto>> ObtenerProyectosCompletosAsync();
    Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null);
}