using MyChamba.Models;

namespace MyChamba.Infrastructure.Models;

public class Solicitude
{
    public uint Id { get; set; }

    public ulong IdEstudiante { get; set; }

    public uint IdProyecto { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public string ResumenHabilidades { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();
}