using MyChamba.DTOs.Proyecto;

namespace MyChamba.Data.Interface;

public interface IProyectoRepository
{
    Task<IEnumerable<ProyectoEmpresaDTO>> ObtenerProyectosPorEmpresaAsync(uint idEmpresa);
}