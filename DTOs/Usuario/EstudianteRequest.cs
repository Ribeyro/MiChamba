namespace MyChamba.DTOs.Usuario;

public class EstudianteRequest
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Telefono { get; set; }
    public int IdUniversidad { get; set; }
    public int IdCarrera { get; set; }
}