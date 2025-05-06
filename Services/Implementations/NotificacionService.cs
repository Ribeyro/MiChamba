using MyChamba.Data.UnitofWork;
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
}