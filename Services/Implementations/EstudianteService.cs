using MyChamba.Data.Interface;
using MyChamba.DTOs.Idioma;
using MyChamba.Models;
using MyChamba.Services.Interfaces;

namespace MyChamba.Services.Implementations;

public class EstudianteIdiomaService : IEstudianteIdiomaService
{
    private readonly IEstudianteIdiomaRepository _idiomaRepository;

    public EstudianteIdiomaService(IEstudianteIdiomaRepository idiomaRepository)
    {
        _idiomaRepository = idiomaRepository;
    }

    public async Task AgregarIdiomasAsync(ulong estudianteId, List<IdiomaRequest> idiomas)
    {
        var entidades = idiomas.Select(i => new EstudianteIdioma
        {
            IdEstudiante = estudianteId,
            IdIdioma = i.IdIdioma,
            Nivel = i.Nivel
        }).ToList();

        await _idiomaRepository.AgregarRangoAsync(entidades);
    }

    public async Task<List<IdiomaResponse>> ObtenerIdiomasAsync(ulong estudianteId)
    {
        var entidades = await _idiomaRepository.ObtenerPorEstudianteAsync(estudianteId);

        return entidades.Select(ei => new IdiomaResponse
        {
            IdIdioma = ei.IdIdioma,
            NombreIdioma = ei.IdIdiomaNavigation.Idioma1,
            Nivel = ei.Nivel
        }).ToList();
    }
}