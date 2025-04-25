using MyChamba.Models;

namespace MyChamba.Data.Interface;

public interface IHabilidadRepository
{
    Task<Habilidade?> ObtenerPorIdAsync(uint id);
    Task<IEnumerable<Habilidade>> ObtenerPorIdsAsync(IEnumerable<uint> ids);
}