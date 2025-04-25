using MyChamba.Data.Repositories;
using MyChamba.Models;
using MyChamba.DTOs.Proyecto;
using MyChamba.Data.UnitofWork;
using System.Threading.Tasks;
using MyChamba.Data.Interface;
using Microsoft.EntityFrameworkCore;



namespace MyChamba.Services.Implementations
{
    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoRecompensaRepository _tipoRecompensaRepository;
        private readonly IHabilidadRepository _habilidadRepository;

        public ProyectoService(
            IProyectoRepository proyectoRepository,
            ITipoRecompensaRepository tipoRecompensaRepository,
            IHabilidadRepository habilidadRepository,
            IUnitOfWork unitOfWork)
        {
            _proyectoRepository = proyectoRepository;
            _tipoRecompensaRepository = tipoRecompensaRepository;
            _habilidadRepository = habilidadRepository;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Crea un nuevo proyecto
        /// </summary>
        /// <param name="dto">Datos para crear el proyecto</param>
        /// <returns>Mensaje de éxito o error</returns>
        public async Task<string> CrearProyectoAsync(CrearProyectoDto dto)
        {
            // Validación de formato de fecha
            if (!DateTime.TryParseExact(dto.FechaLimite, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fechaLimite))
            {
                return "La fecha debe estar en el formato dd/MM/yyyy.";
            }

            // Validación de tipo de recompensa
            if (dto.IdTipoRecompensa > int.MaxValue)
            {
                return "El ID del tipo de recompensa excede el valor permitido.";
            }

            // Validamos que exista el tipo de recompensa
            var tipoRecompensa = await _tipoRecompensaRepository.ObtenerPorIdAsync(dto.IdTipoRecompensa);  // ✅ limpio y tipado correctamente
            if (tipoRecompensa == null)
            {
                return "El tipo de recompensa especificado no existe.";
            }

            // Crear el nuevo proyecto
            var nuevoProyecto = new Proyecto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                FechaLimite = fechaLimite,
                Estado = true,
                NumeroParticipantes = dto.NumeroParticipantes,
                IdEmpresa = dto.IdEmpresa,
                IdTipoRecompensa = dto.IdTipoRecompensa
            };

            // Agregar el proyecto a la base de datos
            await _proyectoRepository.AgregarProyectoAsync(nuevoProyecto);
            await _unitOfWork.Complete();  // Confirmar los cambios en la base de datos

            return "Proyecto creado con éxito.";
        }
        
        //AsociarHabilidades
        public async Task<string> AsociarHabilidadesAsync(uint idProyecto, List<uint> idHabilidades)
        {
            if (idHabilidades == null || !idHabilidades.Any())
                return "Debe proporcionar al menos una habilidad para asociar.";

            var proyecto = await _proyectoRepository.ObtenerPorIdAsync(idProyecto);
            if (proyecto == null)
                return $"Proyecto con ID {idProyecto} no encontrado.";

            var habilidades = await _habilidadRepository.ObtenerPorIdsAsync(idHabilidades);
            if (habilidades.Count() != idHabilidades.Count)
                return "Una o más habilidades no fueron encontradas.";

            foreach (var habilidad in habilidades)
            {
                if (!proyecto.IdHabilidads.Contains(habilidad))
                {
                    proyecto.IdHabilidads.Add(habilidad);
                }
            }

            await _unitOfWork.Complete();
            return $"Habilidades asociadas correctamente al proyecto {proyecto.Nombre}.";
        }
        
        public async Task<List<ProyectoCompletoDto>> ObtenerProyectosCompletosAsync()
        {
            // ❗ Usamos directamente ProyectoRepository, no UnitOfWork aquí
            var proyectos = await _proyectoRepository.ObtenerProyectosConDetallesAsync();

            return proyectos.Select(p => new ProyectoCompletoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                FechaLimite = p.FechaLimite,
                TipoRecompensa = p.IdTipoRecompensaNavigation?.Tipo,
                Habilidades = p.IdHabilidads.Select(h => new HabilidadDto
                {
                    Id = h.Id,
                    Nombre = h.Nombre
                }).ToList()
            }).ToList();
        }
        
        public async Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null)
        {
            if (!DateTime.TryParseExact(fechaTexto, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
                throw new ArgumentException("La fecha debe estar en el formato dd/MM/yyyy.");

            var proyectos = await _proyectoRepository.ObtenerProyectosConDetallesAsync();

            // Filtro por fecha (solo día/mes/año)
            proyectos = proyectos.Where(p => p.FechaLimite.Date == fecha.Date).ToList();

            // Filtro opcional por empresa
            if (idEmpresa.HasValue)
                proyectos = proyectos.Where(p => p.IdEmpresa == idEmpresa.Value).ToList();

            return proyectos.Select(p => new ProyectoCompletoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                FechaLimite = p.FechaLimite,
                TipoRecompensa = p.IdTipoRecompensaNavigation?.Tipo,
                Habilidades = p.IdHabilidads.Select(h => new HabilidadDto
                {
                    Id = h.Id,
                    Nombre = h.Nombre
                }).ToList()
            }).ToList();
        }

        

    }
}
