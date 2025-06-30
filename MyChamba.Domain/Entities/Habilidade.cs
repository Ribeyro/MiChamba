namespace MyChamba.Models;

public class Habilidade
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<EstudianteHabilidad> EstudianteHabilidads { get; set; } =
        new List<EstudianteHabilidad>();

    public virtual ICollection<Proyecto> IdProyectos { get; set; } = new List<Proyecto>();
}