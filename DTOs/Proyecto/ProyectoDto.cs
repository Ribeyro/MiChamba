namespace MyChamba.DTOs.Proyecto;

public class ProyectoDto
{
    public uint Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaLimite { get; set; }
    public ICollection<HabilidadDto> Habilidades { get; set; } // solo las habilidades necesarias
}