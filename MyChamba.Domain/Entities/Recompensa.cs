namespace MyChamba.Models;

public class Recompensa
{
    public uint Id { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public uint IdProyecto { get; set; }

    public uint IdTipoRecompensa { get; set; }

    public ulong IdEstudiante { get; set; }

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual TipoRecompensa IdTipoRecompensaNavigation { get; set; } = null!;

    public virtual RecompensasCertificado? RecompensasCertificado { get; set; }

    public virtual ICollection<RecompensasEconomica> RecompensasEconomicas { get; set; } =
        new List<RecompensasEconomica>();
}