using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.DTOs.Notificaciones;
using MyChamba.Infrastructure.Models;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Notificaciones.CrearNotificacion;

public interface ICrearNotificacionUseCase
{
    Task CrearNotificacionNuevaSolicitudAsync(Solicitude solicitud, ulong idEmpresa, string resumenHabilidades);
    Task<IEnumerable<NotificacionDto>> ObtenerPorEmpresaAsync(ulong idEmpresa);
    Task<IEnumerable<NotificacionDto>> ObtenerPorEstudianteAsync(ulong idEstudiante);
}

public class CrearNotificacionUseCase(IUnitOfWork _unitOfWork) : ICrearNotificacionUseCase
{
    public async Task CrearNotificacionNuevaSolicitudAsync(Solicitude solicitud, ulong idEmpresa,
        string resumenHabilidades)
    {
        var notificacion = new Notificacione
        {
            IdSolicitud = solicitud.Id,
            IdReceptor = idEmpresa,
            TipoMensaje = "nueva_solicitud",
            FechaEnvio = DateTime.Now,
            Mensaje = resumenHabilidades,
            Leido = false
        };

        await _unitOfWork.Repository<Notificacione>().AddAsync(notificacion);
        await _unitOfWork.Complete();
    }

    public async Task<IEnumerable<NotificacionDto>> ObtenerPorEmpresaAsync(ulong idEmpresa)
    {
        var notificaciones = await _unitOfWork.Repository<Notificacione>()
            .FindAsync(n => n.IdReceptor == idEmpresa);

        return notificaciones.Select(n => new NotificacionDto
        {
            Id = n.Id,
            TipoMensaje = n.TipoMensaje,
            Mensaje = n.Mensaje,
            FechaEnvio = n.FechaEnvio,
            Leido = n.Leido
        }).ToList();
    }

    public async Task<IEnumerable<NotificacionDto>> ObtenerPorEstudianteAsync(ulong idEstudiante)
    {
        var solicitudes = await _unitOfWork.Repository<Solicitude>()
            .FindAsync(s => s.IdEstudiante == idEstudiante);

        var solicitudesIds = solicitudes.Select(s => s.Id).ToList();

        var notificaciones = await _unitOfWork.Repository<Notificacione>()
            .FindAsync(n => solicitudesIds.Contains(n.IdSolicitud));

        var solicitudesDict = solicitudes.ToDictionary(s => s.Id, s => s);

        return notificaciones.Select(n => new NotificacionDto
        {
            Id = n.Id,
            IdReceptor = n.IdReceptor,
            IdSolicitud = n.IdSolicitud,
            TipoMensaje = n.TipoMensaje,
            Mensaje = n.Mensaje,
            FechaEnvio = n.FechaEnvio,
            Leido = n.Leido,
            IdProyecto = solicitudesDict.TryGetValue(n.IdSolicitud, out var solicitud)
                ? solicitud.IdProyecto
                : 0
        }).ToList();
    }
}