namespace MyChamba.DTOs.Solicitud;

public class SolicitudRespuestaDto
{
    public uint Id { get; set; }
    public ulong IdEstudiante { get; set; }
    public uint IdProyecto { get; set; }
    public DateTime FechaSolicitud { get; set; }
    public string ResumenHabilidades { get; set; }
    public string Estado { get; set; }
}