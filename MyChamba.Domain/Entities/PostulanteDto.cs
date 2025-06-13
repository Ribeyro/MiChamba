namespace MyChamba.Domain.Entities;

public class PostulanteDto
{
    public uint IdSolicitud { get; set; }
    public ulong IdUsuario { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Universidad { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;
    public string AcercaDe { get; set; } = string.Empty;
    public string EstadoSolicitud { get; set; } = string.Empty;
}