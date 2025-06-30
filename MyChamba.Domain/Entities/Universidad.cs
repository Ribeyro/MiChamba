namespace MyChamba.Models;

public class Universidad
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}