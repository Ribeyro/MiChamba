namespace MyChamba.DTOs.Usuario;

public class EmpresaRequest
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public string Ruc { get; set; }
    public string Logo { get; set; }
    public int IdSector { get; set; }
}