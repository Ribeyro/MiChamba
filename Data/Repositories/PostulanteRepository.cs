using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyChamba.Data.Interface;
using MyChamba.DTOs.Student;
using MyChamba.Models;

namespace MyChamba.Data.Repositories
{
    public class PostulanteRepository : IPostulanteRepository
    {
        private readonly MyChambaContext _context;

        public PostulanteRepository(MyChambaContext context)
        {
            _context = context;
        }

        public async Task<List<PostulanteDto>> ObtenerPostulantesPorProyectoAsync(uint idProyecto)
        {
            var query = from s in _context.Solicitudes
                join e in _context.Estudiantes on s.IdEstudiante equals e.IdUsuario
                join u in _context.Usuarios on e.IdUsuario equals u.Id
                join c in _context.Carreras on e.IdCarrera equals c.Id into carreraJoin
                from carrera in carreraJoin.DefaultIfEmpty()
                join un in _context.Universidads on e.IdUniversidad equals un.Id into uniJoin
                from universidad in uniJoin.DefaultIfEmpty()
                join p in _context.PerfilEstudiantes on e.IdUsuario equals p.IdEstudiante into perfilJoin
                from perfil in perfilJoin.DefaultIfEmpty()
                where s.IdProyecto == idProyecto
                select new PostulanteDto
                {
                    IdUsuario = u.Id,
                    NombreCompleto = e.Nombre + " " + e.Apellido,
                    Email = u.Email,
                    Universidad = universidad != null ? universidad.Nombre : "",
                    Carrera = carrera != null ? carrera.Nombre : "",
                    AcercaDe = perfil != null ? perfil.AcercaDe : "",
                    EstadoSolicitud = s.Estado
                };

            return await query.ToListAsync();
        }
    }
}