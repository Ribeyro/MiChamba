namespace MyChamba.DTOs.Proyecto;

public class ProyectoCompletoDto
{
    public uint Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaLimite { get; set; }
    public string? TipoRecompensa { get; set; }
    public List<HabilidadDto> Habilidades { get; set; } = new();

}