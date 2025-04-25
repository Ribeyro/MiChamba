using Microsoft.EntityFrameworkCore;
using MyChamba.Data;
using MyChamba.Data.Interface;
using MyChamba.Data.UnitofWork;
using MyChamba.DTOs.Solicitud;
using MyChamba.Models;
using MyChamba.Services.Interfaces;

namespace MyChamba.Services.Implementations
{
    public class SolicitudService : ISolicitudService
    {
        private readonly ISolicitudRepository _solicitudRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MyChambaContext _context;

        public SolicitudService(ISolicitudRepository solicitudRepository, IUnitOfWork unitOfWork, MyChambaContext context)
        {
            _solicitudRepository = solicitudRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<SolicitudRespuestaDto> CrearSolicitudAsync(CrearSolicitudDto dto)
        {
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.IdUsuario == dto.IdEstudiante);
            if (estudiante == null)
                throw new Exception("El estudiante especificado no existe.");

            var proyecto = await _context.Proyectos
                .FirstOrDefaultAsync(p => p.Id == dto.IdProyecto);
            if (proyecto == null)
                throw new Exception("El proyecto especificado no existe.");

            var solicitudExistente = await _solicitudRepository
                .ObtenerPorEstudianteYProyectoAsync(dto.IdEstudiante, dto.IdProyecto);
            if (solicitudExistente != null)
                throw new Exception("El estudiante ya está postulado a este proyecto.");

            var nuevaSolicitud = new Solicitude
            {
                IdEstudiante = dto.IdEstudiante,
                IdProyecto = dto.IdProyecto,
                ResumenHabilidades = dto.ResumenHabilidades,
                FechaSolicitud = DateTime.UtcNow,
                Estado = "Pendiente"
            };

            await _solicitudRepository.AddAsync(nuevaSolicitud);
            await _unitOfWork.Complete(); // Aquí usamos UnitOfWork

            return new SolicitudRespuestaDto
            {
                Id = nuevaSolicitud.Id,
                IdEstudiante = nuevaSolicitud.IdEstudiante,
                IdProyecto = nuevaSolicitud.IdProyecto,
                FechaSolicitud = nuevaSolicitud.FechaSolicitud,
                ResumenHabilidades = nuevaSolicitud.ResumenHabilidades,
                Estado = nuevaSolicitud.Estado
            };
        }
    }
}
