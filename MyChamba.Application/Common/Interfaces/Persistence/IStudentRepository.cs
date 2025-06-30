using MyChamba.DTOs.Proyecto;

namespace MyChamba.Application.Common.Interfaces.Persistence;

public interface IStudentRepository
{
    Task<List<ProyectoCompletoDto>> ObtenerProyectosCompletosAsync();
    Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null);
}