namespace MyChamba.DTOs.Solicitud;

public class CrearSolicitudDto
{
    public ulong IdEstudiante { get; set; }  // ID del estudiante
    public uint IdProyecto { get; set; }     // ID del proyecto
    public string ResumenHabilidades { get; set; }  // Resumen de habilidades del estudiante
}