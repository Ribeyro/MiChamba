using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.UseCases.Notificaciones.CrearNotificacion;
using MyChamba.Infrastructure.Models;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Postulaciones.Commands;

public class PostularEstudianteCommandHandler (IUnitOfWork _unitOfWork, ICrearNotificacionUseCase _crearNotificacionUseCase) : IRequestHandler<PostularEstudianteCommand, bool>
{
    
    public async Task<bool> Handle(PostularEstudianteCommand command, CancellationToken cancellationToken)
    {
        var dto = command.Dto;

        var estudiante = (await _unitOfWork.Repository<Estudiante>()
                .FindAsync(e => e.IdUsuario == dto.IdEstudiante))
            .FirstOrDefault();
        if (estudiante == null)
            throw new Exception("El estudiante no existe.");

        var proyecto = (await _unitOfWork.Repository<Proyecto>()
                .FindAsync(p => p.Id == dto.IdProyecto))
            .FirstOrDefault();
        if (proyecto == null)
            throw new Exception("El proyecto no existe.");

        var yaExiste = (await _unitOfWork.Repository<Solicitude>()
                .FindAsync(s => s.IdEstudiante == dto.IdEstudiante && s.IdProyecto == dto.IdProyecto))
            .Any();
        if (yaExiste)
            throw new Exception("Ya te has postulado a este proyecto.");

        var solicitud = new Solicitude
        {
            IdEstudiante = dto.IdEstudiante,
            IdProyecto = dto.IdProyecto,
            ResumenHabilidades = dto.ResumenHabilidades,
            FechaSolicitud = DateTime.UtcNow,
            Estado = "Pendiente"
        };

        await _unitOfWork.Repository<Solicitude>().AddAsync(solicitud);
        await _unitOfWork.Complete();

        var idEmpresa = proyecto.IdEmpresa;

        await _crearNotificacionUseCase
            .CrearNotificacionNuevaSolicitudAsync(solicitud, idEmpresa, dto.ResumenHabilidades);

        return true;
    }
}