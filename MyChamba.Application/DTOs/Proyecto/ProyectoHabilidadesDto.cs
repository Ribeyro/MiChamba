namespace MyChamba.DTOs.Proyecto;

public class ProyectoHabilidadesDto
{
    public uint ProyectoId { get; set; }
    public List<uint> IdHabilidades { get; set; } = new();
}