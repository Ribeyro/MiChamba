using MyChamba.DTOs.Proyecto;

namespace MyChamba.Application.DTOs.Student;

public class EstudianteProfileDto
{
    public ulong IdUsuario { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string? Avatar { get; set; }
    public string? AcercaDe { get; set; }
    public string? NombreCarrera { get; set; }
    public string? NombreUniversidad { get; set; }
    public IEnumerable<HabilidadDto>? Habilidades { get; set; }
    public IEnumerable<IdiomaDto>? Idiomas { get; set; }
    public IEnumerable<LinkDto>? Links { get; set; }
}