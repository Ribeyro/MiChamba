namespace MyChamba.DTOs.Notificaciones;

public class NotificacionDto
{
    public uint Id { get; set; }
    public string TipoMensaje { get; set; } = null!;
    public string Mensaje { get; set; } = null!;
    public DateTime FechaEnvio { get; set; }
    public bool Leido { get; set; }
}