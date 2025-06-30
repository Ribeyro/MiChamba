namespace MyChamba.Application.DTOs.Student;

public class UpdateEstudianteProfileDto
{
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Telefono { get; set; }
    public string? Avatar { get; set; }
    public string? AcercaDe { get; set; }
    public uint? IdCarrera { get; set; }
    public uint? IdUniversidad { get; set; }
    public IEnumerable<IdiomaUpdateDto>? Idiomas { get; set; }
    public IEnumerable<HabilidadUpdateDto>? Habilidades { get; set; }
    public IEnumerable<LinkUpdateDto>? Links { get; set; }
}

public class IdiomaUpdateDto
{
    public uint IdIdioma { get; set; }
    public string Nivel { get; set; }
}

public class HabilidadUpdateDto
{
    public uint IdHabilidad { get; set; }
}

public class LinkUpdateDto
{
    public string TipoLink { get; set; }
    public string Url { get; set; }
}