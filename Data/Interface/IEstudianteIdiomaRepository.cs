using MyChamba.Models;

namespace MyChamba.Data.Interface
{
    public interface IEstudianteIdiomaRepository
    {
        Task<List<EstudianteIdioma>> ObtenerPorEstudianteAsync(ulong estudianteId);
        Task AgregarRangoAsync(List<EstudianteIdioma> idiomas);
    }
}