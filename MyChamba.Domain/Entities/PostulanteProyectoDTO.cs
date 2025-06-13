namespace MyChamba.Domain.Entities;

public class PostulanteProyectoDTO
{
    public uint IdEstudiante { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Carrera { get; set; } = null!;
    public string Universidad { get; set; } = null!;
    public string EstadoSolicitud { get; set; } = null!;
}