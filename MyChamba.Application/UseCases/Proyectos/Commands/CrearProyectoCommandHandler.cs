using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Infrastructure.Models;
using MyChamba.Models;

namespace MyChamba.Services.Implementations.Commands;

public class CrearProyectoCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CrearProyectoCommand, bool>
{
    public async Task<bool> Handle(CrearProyectoCommand command, CancellationToken cancellationToken)
    {
        var dto = command.Dto;

        var empresa = (await _unitOfWork.Repository<Empresa>()
            .FindAsync(e => e.IdUsuario == dto.IdEmpresa)).FirstOrDefault()
            ?? throw new Exception("La empresa no existe");

        var tipoRecompensa = (await _unitOfWork.Repository<TipoRecompensa>()
            .FindAsync(t => t.Id == dto.TipoRecompensa)).FirstOrDefault()
            ?? throw new Exception("El tipo de recompensa no existe");

        var habilidadesRepo = _unitOfWork.Repository<Habilidade>();
        var habilidades = new List<Habilidade>();

        foreach (var idHab in dto.IdHabilidades)
        {
            var hab = (await habilidadesRepo.FindAsync(h => h.Id == idHab)).FirstOrDefault()
                      ?? throw new Exception($"La habilidad con ID {idHab} no existe");
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