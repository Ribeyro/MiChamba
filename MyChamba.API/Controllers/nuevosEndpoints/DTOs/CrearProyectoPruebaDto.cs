namespace MiChamba.API.Controllers.DTOs;

public class CrearProyectoPruebaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaLimite { get; set; }
    public uint IdEmpresa { get; set; }
    public uint IdTipoRecompensa { get; set; }

    // Mezcla de ID (int) y nombres (string)
    public List<object> IdHabilidades { get; set; } = new();
}