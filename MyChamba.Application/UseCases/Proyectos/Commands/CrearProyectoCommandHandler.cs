using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Infrastructure.Models;
using MyChamba.Models;

namespace MyChamba.Services.Implementations.Commands;

public class CrearProyectoCommandHandler: IRequestHandler<CrearProyectoCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public CrearProyectoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CrearProyectoCommand dto, CancellationToken cancellationToken)
    {
        var empresa = (await _unitOfWork.Repository<Empresa>()
            .FindAsync(e => e.IdUsuario == dto.IdEmpresa)).FirstOrDefault();

        if (empresa == null)
            throw new Exception("La empresa no existe");

        var tipoRecompensa = (await _unitOfWork.Repository<TipoRecompensa>()
            .FindAsync(t => t.Id == dto.TipoRecompensa)).FirstOrDefault();

        if (tipoRecompensa == null)
            throw new Exception("El tipo de recompensa no existe");

        var habilidadesRepo = _unitOfWork.Repository<Habilidade>();
        var habilidades = new List<Habilidade>();

        foreach (var idHab in dto.IdHabilidades)
        {
            var hab = (await habilidadesRepo.FindAsync(h => h.Id == idHab)).FirstOrDefault();
            if (hab == null)
                throw new Exception($"La habilidad con ID {idHab} no existe");

            habilidades.Add(hab);
        }

        if (dto.FechaLimite.Date <= DateTime.UtcNow.Date)
            throw new Exception("La fecha límite debe ser mayor al día actual.");

        var proyecto = new Proyecto
        {
            Nombre = dto.Titulo,
            Descripcion = dto.Descripcion,
            FechaLimite = dto.FechaLimite,
            Estado = true,
            NumeroParticipantes = 0,
            IdEmpresa = dto.IdEmpresa,
            IdTipoRecompensa = dto.TipoRecompensa,
            IdHabilidads = habilidades
        };

        await _unitOfWork.Repository<Proyecto>().AddAsync(proyecto);
        await _unitOfWork.Complete();

        return true;
    }
}