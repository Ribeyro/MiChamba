using MediatR;

namespace MyChamba.Services.Implementations.Commands;



public class CrearProyectoCommand : IRequest<bool>
{
    public string Titulo { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public DateTime FechaLimite { get; set; }
    public uint TipoRecompensa { get; set; }
    public uint IdEmpresa { get; set; }
    public List<uint> IdHabilidades { get; set; } = new();
}
