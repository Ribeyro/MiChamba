using MyChamba.DTOs.Solicitud;

namespace MyChamba.Services.Interfaces;

public interface ISolicitudService
{
    Task<bool> PostularEstudianteAsync(CrearSolicitudDto dto);
}