namespace MyChamba.Models;

public class TipoRecompensa
{
    public uint Id { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();

    public virtual ICollection<Recompensa> Recompensas { get; set; } = new List<Recompensa>();
}