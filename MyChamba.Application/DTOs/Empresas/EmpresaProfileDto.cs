namespace MyChamba.Application.DTOs.Empresas;

public class EmpresaProfileDto
{
    public ulong IdUsuario { get; set; }
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Direccion { get; set; } = null!;
    public string Ruc { get; set; } = null!;
    public string Logo { get; set; } = null!;
    public string Sector { get; set; } = null!;
}