namespace MyChamba.DTOs.Proyectos;

public class EntregaProyectoCreateDto
{
    public ulong IdEstudiante { get; set; }
    public uint IdProyecto { get; set; }
    public string Descripcion { get; set; } = null!;
    public string Link { get; set; } = null!;
}