using MyChamba.DTOs.Idioma;

namespace MyChamba.DTOs.Usuario;

public class EstudianteRequest
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Telefono { get; set; }
    public int IdUniversidad { get; set; }
    public int IdCarrera { get; set; }
    //Agregar idioma
    //nivel
    //trabajar con la tabla intermedia Estudiante_Idioma
    
    // Nuevo:
    public List<IdiomaRequest> Idiomas { get; set; } = new();
}