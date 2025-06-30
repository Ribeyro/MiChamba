using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Student;
using MyChamba.Application.UseCases.Notificaciones.CrearNotificacion;
using MyChamba.DTOs.Solicitud;
using MyChamba.Infrastructure.Models;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Postulaciones.PostularEstudiante;


public interface IPostularEstudianteUseCase
{
    Task<bool> PostularEstudianteAsync(CrearSolicitudDto dto);
}
public class PostularEstudianteUseCase (IUnitOfWork _unitOfWork, ICrearNotificacionUseCase _crearNotificacionUseCase): IPostularEstudianteUseCase
{
    public async Task<bool> PostularEstudianteAsync(CrearSolicitudDto dto)
    {
        // Validar que el estudiante exista
        var estudiante = (await _unitOfWork.Repository<Estudiante>().FindAsync(e => e.IdUsuario == dto.IdEstudiante))
            .FirstOrDefault();
        if (estudiante == null)
            throw new Exception("El estudiante no existe.");

        // Validar que el proyecto exista
        var proyecto =
            (await _unitOfWork.Repository<Proyecto>().FindAsync(p => p.Id == dto.IdProyecto)).FirstOrDefault();
        if (proyecto == null)
            throw new Exception("El proyecto no existe.");

        // Validar que no exista una solicitud previa
        var yaExiste = (await _unitOfWork.Repository
            <Solicitude>().FindAsync(s =>
            s.IdEstudiante == dto.IdEstudiante
            && s.IdProyecto == dto.IdProyecto)).Any();

        if (yaExiste)
            throw new Exception("Ya te has postulado a este proyecto.");

        // Crear nueva solicitud
        var solicitud =
            new Solicitude
            {
                IdEstudiante = dto.IdEstudiante,
                IdProyecto = dto.IdProyecto,
                ResumenHabilidades = dto.ResumenHabilidades,
                FechaSolicitud = DateTime.UtcNow,
                Estado = "Pendiente" // Estado inicial
            };

        await _unitOfWork.Repository<Solicitude>().AddAsync(solicitud);
        await _unitOfWork.Complete();


        // Obtener ID del usuario empresa dueña del proyecto
        var idEmpresa = proyecto.IdEmpresa; // Asegúrate que el modelo ProyectoDto tenga esta propiedad

        // Crear la notificación
        await _crearNotificacionUseCase.CrearNotificacionNuevaSolicitudAsync(solicitud, idEmpresa, dto.ResumenHabilidades);

        return true;
    }
}


//Task<List<PostulanteDto>> ObtenerPostulantesPorProyectoAsync(uint idProyecto);
//Task AceptarPostulanteAsync(uint idSolicitud);