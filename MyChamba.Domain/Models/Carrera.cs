using MyChamba.Models;

namespace MyChamba.Infrastructure.Models;

public partial class Carrera
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
