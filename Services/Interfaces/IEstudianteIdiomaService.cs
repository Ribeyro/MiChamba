using MyChamba.DTOs.Idioma;

namespace MyChamba.Services.Interfaces;

public interface IEstudianteIdiomaService
{
    Task AgregarIdiomasAsync(ulong estudianteId, List<IdiomaRequest> idiomas);
    Task<List<IdiomaResponse>> ObtenerIdiomasAsync(ulong estudianteId);
}