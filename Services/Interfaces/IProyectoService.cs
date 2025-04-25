

using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Implementations;


public interface IProyectoService
{
    Task<string> CrearProyectoAsync(CrearProyectoDto dto);
    Task<string> AsociarHabilidadesAsync(uint idProyecto, List<uint> idHabilidades);
    
    Task<List<ProyectoCompletoDto>> ObtenerProyectosCompletosAsync();
    
    Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null);



}
