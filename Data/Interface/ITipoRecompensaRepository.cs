// En MyChamba.Data.Repositories
using MyChamba.Models;
using System.Threading.Tasks;

public interface ITipoRecompensaRepository
{
    Task<TipoRecompensa?> ObtenerPorIdAsync(uint id);
}