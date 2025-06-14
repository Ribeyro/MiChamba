namespace MyChamba.Models;

public class Link
{
    public uint Id { get; set; }

    public ulong IdEstudiante { get; set; }

    public string TipoLink { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;
}