using MyChamba.DTOs.Proyecto;

namespace MyChamba.Application.Common.Interfaces.Persistence;

public interface IProyectoRepository
{
    Task<IEnumerable<ProyectoEmpresaDTO>> ObtenerProyectosPorEmpresaAsync(uint idEmpresa);
}