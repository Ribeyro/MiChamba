using MyChamba.DTOs.Solicitud;

namespace MyChamba.Services.Interfaces
{
    public interface ISolicitudService
    {
        Task<SolicitudRespuestaDto> CrearSolicitudAsync(CrearSolicitudDto dto);
    }
}