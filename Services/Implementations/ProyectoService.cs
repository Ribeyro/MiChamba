using MyChamba.Data.UnitofWork;
using MyChamba.DTOs.Proyecto;
using MyChamba.Models;
using MyChamba.Services.Interfaces;

namespace MyChamba.Services.Implementations;

public class ProyectoService : IProyectoService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProyectoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CrearProyectoAsync(CrearProyectoDTO dto)
    {
        // Validar que la empresa exista
        var empresa = (await _unitOfWork.Repository<Empresa>().FindAsync(e => e.IdUsuario == (ulong)dto.IdEmpresa)).FirstOrDefault();
        if (empresa == null)
            throw new Exception("La empresa no existe");

        // Validar que el tipo de recompensa exista
        var tipoRecompensa = (await _unitOfWork.Repository<TipoRecompensa>().FindAsync(t => t.Id == (uint)dto.TipoRecompensa)).FirstOrDefault();
        if (tipoRecompensa == null)
            throw new Exception("El tipo de recompensa no existe");

        // Validar que todas las habilidades existan
        var habilidadesRepo = _unitOfWork.Repository<Habilidade>();
        var habilidades = new List<Habilidade>();

        foreach (var idHab in dto.IdHabilidades)
        {
            var hab = (await habilidadesRepo.FindAsync(h => h.Id == (uint)idHab)).FirstOrDefault();
            if (hab == null)
                throw new Exception($"La habilidad con ID {idHab} no existe");

            habilidades.Add(hab);
        }
        // ✅ Validar que la fecha límite sea posterior al día actual
        if (dto.FechaLimite.Date <= DateTime.UtcNow.Date)
            throw new Exception("La fecha límite debe ser mayor al día actual.");


        // Crear objeto Proyecto
        var proyecto = new Proyecto
        {
            Nombre = dto.Titulo,
            Descripcion = dto.Descripcion,
            FechaLimite = dto.FechaLimite,
            Estado = true,
            NumeroParticipantes = 0,
            IdEmpresa = dto.IdEmpresa,
            IdTipoRecompensa = dto.TipoRecompensa,
            IdHabilidads = habilidades // Relación muchos a muchos
        };

        await _unitOfWork.Repository<Proyecto>().AddAsync(proyecto);
        await _unitOfWork.Complete();

        return true;
    }
}