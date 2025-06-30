using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Student;
using MyChamba.Data;
using MyChamba.DTOs.Proyecto;
using MyChamba.Models;

namespace MyChamba.Infrastructure.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly MyChambaContext _context;

    public StudentRepository(MyChambaContext context)
    {
        _context = context;
    }

    public async Task<List<ProyectoCompletoDto>> ObtenerProyectosCompletosAsync()
    {
        var proyectos = await _context.Proyectos
            .Include(p => p.IdTipoRecompensaNavigation)
            .Include(p => p.IdHabilidads)
            .ToListAsync();

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

    public async Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto,
        uint? idEmpresa = null)
    {
        if (!DateTime.TryParseExact(fechaTexto, "dd/MM/yyyy", null, DateTimeStyles.None, out var fecha))
            throw new ArgumentException("La fecha debe estar en el formato dd/MM/yyyy.");

        var proyectos = await _context.Proyectos
            .Where(p => p.FechaLimite.Date == fecha.Date)
            .Include(p => p.IdTipoRecompensaNavigation)
            .Include(p => p.IdHabilidads)
            .ToListAsync();

        if (idEmpresa.HasValue) proyectos = proyectos.Where(p => p.IdEmpresa == idEmpresa.Value).ToList();

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
    
    public async Task<EstudianteProfileDto> GetProfileAsync(ulong idUsuario)
    {
        var usuario = await _context.Usuarios.FindAsync(idUsuario);
        if (usuario == null) return null;

        var estudiante = await _context.Estudiantes
            .Include(e => e.IdCarreraNavigation)
            .Include(e => e.IdUniversidadNavigation)
            .FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);

        if (estudiante == null) return null;

        var perfil = await _context.PerfilEstudiantes.FindAsync(idUsuario);

        var habilidades = await _context.EstudianteHabilidads
            .Include(eh => eh.IdHabilidadNavigation)
            .Where(eh => eh.IdEstudiante == idUsuario)
            .ToListAsync();

        var idiomas = await _context.EstudianteIdiomas
            .Include(ei => ei.IdIdiomaNavigation)
            .Where(ei => ei.IdEstudiante == idUsuario)
            .ToListAsync();

        var links = await _context.Links
            .Where(l => l.IdEstudiante == idUsuario)
            .ToListAsync();

        return new EstudianteProfileDto
        {
            IdUsuario = usuario.Id,
            Nombre = estudiante.Nombre,
            Apellido = estudiante.Apellido,
            Telefono = estudiante.Telefono,
            Email = usuario.Email,
            Avatar = perfil?.Avatar,
            AcercaDe = perfil?.AcercaDe,
            NombreCarrera = estudiante.IdCarreraNavigation?.Nombre,
            NombreUniversidad = estudiante.IdUniversidadNavigation?.Nombre,
            Habilidades = habilidades.Select(h => new HabilidadDto
            {
                Id = h.IdHabilidad,
                Nombre = h.IdHabilidadNavigation.Nombre
            }),
            Idiomas = idiomas.Select(i => new IdiomaDto
            {
                IdIdioma = i.IdIdioma,
                Idioma = i.IdIdiomaNavigation.Idioma1,
                Nivel = i.Nivel
            }),
            Links = links.Select(l => new LinkDto
            {
                Id = l.Id,
                TipoLink = l.TipoLink,
                Url = l.Url
            })
        };
    }

    public async Task UpdateProfileAsync(ulong idUsuario, UpdateEstudianteProfileDto profileDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            // Actualizar datos básicos del estudiante
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);
            if (estudiante != null)
            {
                if (!string.IsNullOrEmpty(profileDto.Nombre))
                    estudiante.Nombre = profileDto.Nombre;
                
                if (!string.IsNullOrEmpty(profileDto.Apellido))
                    estudiante.Apellido = profileDto.Apellido;
                
                if (!string.IsNullOrEmpty(profileDto.Telefono))
                    estudiante.Telefono = profileDto.Telefono;
                
                if (profileDto.IdCarrera.HasValue)
                    estudiante.IdCarrera = profileDto.IdCarrera.Value;
                
                if (profileDto.IdUniversidad.HasValue)
                    estudiante.IdUniversidad = profileDto.IdUniversidad.Value;
            }

            // Actualizar perfil
            var perfil = await _context.PerfilEstudiantes.FindAsync(idUsuario);
            if (perfil == null)
            {
                perfil = new PerfilEstudiante { IdEstudiante = idUsuario };
                _context.PerfilEstudiantes.Add(perfil);
            }

            if (!string.IsNullOrEmpty(profileDto.Avatar))
                perfil.Avatar = profileDto.Avatar;
            
            if (!string.IsNullOrEmpty(profileDto.AcercaDe))
                perfil.AcercaDe = profileDto.AcercaDe;

            // Actualizar idiomas
            if (profileDto.Idiomas != null)
            {
                var existingIdiomas = await _context.EstudianteIdiomas
                    .Where(ei => ei.IdEstudiante == idUsuario)
                    .ToListAsync();

                _context.EstudianteIdiomas.RemoveRange(existingIdiomas);

                foreach (var idiomaDto in profileDto.Idiomas)
                {
                    _context.EstudianteIdiomas.Add(new EstudianteIdioma
                    {
                        IdEstudiante = idUsuario,
                        IdIdioma = idiomaDto.IdIdioma,
                        Nivel = idiomaDto.Nivel
                    });
                }
            }

            // Actualizar habilidades
            if (profileDto.Habilidades != null)
            {
                var existingHabilidades = await _context.EstudianteHabilidads
                    .Where(eh => eh.IdEstudiante == idUsuario)
                    .ToListAsync();

                _context.EstudianteHabilidads.RemoveRange(existingHabilidades);

                foreach (var habilidadDto in profileDto.Habilidades)
                {
                    _context.EstudianteHabilidads.Add(new EstudianteHabilidad
                    {
                        IdEstudiante = idUsuario,
                        IdHabilidad = habilidadDto.IdHabilidad
                    });
                }
            }

            // Actualizar links
            if (profileDto.Links != null)
            {
                var existingLinks = await _context.Links
                    .Where(l => l.IdEstudiante == idUsuario)
                    .ToListAsync();

                _context.Links.RemoveRange(existingLinks);

                foreach (var linkDto in profileDto.Links)
                {
                    _context.Links.Add(new Link
                    {
                        IdEstudiante = idUsuario,
                        TipoLink = linkDto.TipoLink,
                        Url = linkDto.Url
                    });
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}