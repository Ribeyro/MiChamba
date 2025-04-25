using MyChamba.Models;

namespace MyChamba.Data.Repositories;

public interface IProyectoRepository
{
    Task<Proyecto?> ObtenerPorIdAsync(uint id);
    Task AgregarProyectoAsync(Proyecto proyecto);
    
    Task<List<Proyecto>> ObtenerProyectosConDetallesAsync();
    
    Task<List<Proyecto>> ObtenerProyectosConDetallesPorFechaYEmpresaAsync(DateTime fecha, uint idEmpresa);

    
    

}