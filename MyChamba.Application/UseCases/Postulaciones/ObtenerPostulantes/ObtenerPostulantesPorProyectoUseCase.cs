using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Student;

namespace MyChamba.Application.UseCases.Postulaciones.ObtenerPostulantes;

public interface IObtenerPostulantesPorProyectoUseCase
{
    Task<List<PostulanteDto>> ExecuteAsync(uint idProyecto);
}
public class ObtenerPostulantesPorProyectoUseCase(IPostulanteRepository _postulanteRepository)
    : IObtenerPostulantesPorProyectoUseCase
{
    public async Task<List<PostulanteDto>> ExecuteAsync(uint idProyecto)
    {
        var postulantes = await _postulanteRepository.ObtenerPostulantesPorProyectoAsync(idProyecto);

        return postulantes.Select(p => new PostulanteDto
        {
            IdSolicitud = p.IdSolicitud,
            IdUsuario = p.IdUsuario,
            NombreCompleto = p.NombreCompleto,
            Email = p.Email,
            Universidad = p.Universidad,
            Carrera = p.Carrera,
            AcercaDe = p.AcercaDe,
            EstadoSolicitud = p.EstadoSolicitud
        }).ToList();
    }

}