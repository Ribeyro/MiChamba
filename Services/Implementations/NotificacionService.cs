using MyChamba.Data.UnitofWork;
using MyChamba.DTOs.Notificaciones;
using MyChamba.Models;
using MyChamba.Services.Interfaces;

namespace MyChamba.Services.Implementations;

public class NotificacionService : INotificacionService
{
    private readonly IUnitOfWork _unitOfWork;

    public NotificacionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CrearNotificacionNuevaSolicitudAsync(Solicitude solicitud, ulong idEmpresa, string resumenHabilidades)
    {
        var notificacion = new Notificacione
        {
            IdSolicitud = solicitud.Id,
            IdReceptor = idEmpresa,
            TipoMensaje = "nueva_solicitud",
            FechaEnvio = DateTime.UtcNow,
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
            Leido = n.Leido,
        }).ToList();
    }

    public async Task<IEnumerable<NotificacionDto>> ObtenerPorEstudianteAsync(ulong idEstudiante)
    {
        var solicitudes = await _unitOfWork.Repository<Solicitude>()
            .FindAsync(s => s.IdEstudiante == idEstudiante);

        var solicitudesIds = solicitudes.Select(s => s.Id).ToList();

        var notificaciones = await _unitOfWork.Repository<Notificacione>()
            .FindAsync(n => solicitudesIds.Contains(n.IdSolicitud));

        return notificaciones.Select(n => new NotificacionDto
        {
            Id = n.Id,
            TipoMensaje = n.TipoMensaje,
            Mensaje = n.Mensaje,
            FechaEnvio = n.FechaEnvio,
            Leido = n.Leido,
        }).ToList();
    }
}