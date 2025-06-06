using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Interfaces;

public interface IStudentService
{
    Task<List<ProyectoCompletoDto>> ObtenerProyectosDisponiblesAsync();
    Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null);
}