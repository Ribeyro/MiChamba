using MyChamba.Domain.Entities;

namespace MyChamba.Data.Interface;

public interface IProyectoRepository
{
    Task<IEnumerable<ProyectoEmpresaDTO>> ObtenerProyectosPorEmpresaAsync(uint idEmpresa);
}