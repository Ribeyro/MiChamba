namespace MyChamba.Models;

public class EstudianteIdioma
{
    public uint Id { get; set; }

    public ulong IdEstudiante { get; set; }

    public uint IdIdioma { get; set; }

    public string Nivel { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Idioma IdIdiomaNavigation { get; set; } = null!;
}