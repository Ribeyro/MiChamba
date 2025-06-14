using MyChamba.DTOs.Proyecto;
using MyChamba.Models;

namespace MyChamba.Domain.Entities;

public class ProyectoDto
{
    public uint Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaLimite { get; set; }

    // En lugar de un string, es mejor tener una relación con una entidad Recompensa
    public uint? IdTipoRecompensa { get; set; }
    public TipoRecompensa? TipoRecompensa { get; set; }

    // Relación con habilidades
    public ICollection<HabilidadDto> Habilidades { get; set; } = new List<HabilidadDto>();
}