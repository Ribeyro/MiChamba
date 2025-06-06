using MyChamba.Application.Services.Interfaces;
using MyChamba.Data.Interface;
using MyChamba.Data.UnitofWork;
using MyChamba.DTOs.Proyecto;
using MyChamba.Infrastructure.Models;
using MyChamba.Models;

// Para el repositorio especializado de proyectos
// Para el acceso transaccional a los repositorios
// Para los DTOs de entrada y salida
// Para las entidades de base de datos

// Para la interfaz del servicio

namespace MyChamba.Services.Implementations;

/// <summary>
/// Servicio para gestionar la lógica relacionada con los proyectos de la empresa.
/// Incluye operaciones para listar y crear proyectos.
/// </summary>
public class ProyectoService : IProyectoService
{
    private readonly IProyectoRepository _proyectoRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor que recibe las dependencias necesarias.
    /// </summary>
    public ProyectoService(IProyectoRepository proyectoRepository, IUnitOfWork unitOfWork)
    {
        _proyectoRepository = proyectoRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Lista todos los proyectos creados por una empresa específica,
    /// incluyendo detalles de los postulantes (estudiantes, universidad, carrera).
    /// </summary>
    /// <param name="idEmpresa">ID de la empresa</param>
    /// <returns>Listado de proyectos con datos enriquecidos</returns>
    public async Task<IEnumerable<ProyectoEmpresaDTO>> ListarPorEmpresaAsync(uint idEmpresa)
    {
        return await _proyectoRepository.ObtenerProyectosPorEmpresaAsync(idEmpresa);
    }

    /// <summary>
    /// Crea un nuevo proyecto después de validar la empresa, tipo de recompensa y habilidades.
    /// </summary>
    /// <param name="dto">Datos necesarios para crear el proyecto</param>
    /// <returns>True si la operación fue exitosa</returns>
    /// <exception cref="Exception">Si alguna validación falla</exception>
    public async Task<bool> CrearProyectoAsync(CrearProyectoDTO dto)
    {
        // Validar existencia de la empresa
        var empresa = (await _unitOfWork.Repository<Empresa>()
            .FindAsync(e => e.IdUsuario == (ulong)dto.IdEmpresa)).FirstOrDefault();

        if (empresa == null)
            throw new Exception("La empresa no existe");

        // Validar existencia del tipo de recompensa
        var tipoRecompensa = (await _unitOfWork.Repository<TipoRecompensa>()
            .FindAsync(t => t.Id == (uint)dto.TipoRecompensa)).FirstOrDefault();

        if (tipoRecompensa == null)
            throw new Exception("El tipo de recompensa no existe");

        // Validar existencia de habilidades
        var habilidadesRepo = _unitOfWork.Repository<Habilidade>();
        var habilidades = new List<Habilidade>();

        foreach (var idHab in dto.IdHabilidades)
        {
            var hab = (await habilidadesRepo.FindAsync(h => h.Id == (uint)idHab)).FirstOrDefault();
            if (hab == null)
                throw new Exception($"La habilidad con ID {idHab} no existe");

            habilidades.Add(hab);
        }

        // Validar que la fecha límite sea posterior al día actual
        if (dto.FechaLimite.Date <= DateTime.UtcNow.Date)
            throw new Exception("La fecha límite debe ser mayor al día actual.");

        // Crear y guardar el proyecto
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
