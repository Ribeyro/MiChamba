namespace MyChamba.Models;

public class EntregasProyecto
{
    public uint Id { get; set; }

    public ulong IdEstudiante { get; set; }

    public uint IdProyecto { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Link { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string EstadoEvaluacion { get; set; } = null!;

    public string Comentarios { get; set; } = null!;

    public string Rendimiento { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}