using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyChamba.DTOs.Student;
using MyChamba.Services.Interfaces;
using MyChamba.Data.Repositories;

namespace MyChamba.Services.Implementations
{
    public class PostulanteService : IPostulanteService
    {
        private readonly IPostulanteRepository _postulanteRepository;

        public PostulanteService(IPostulanteRepository postulanteRepository)
        {
            _postulanteRepository = postulanteRepository;
        }

        public async Task<List<PostulanteDto>> ObtenerPostulantesPorProyectoAsync(uint idProyecto)
        {
            // Llamamos al repositorio para obtener los postulantes
            var postulantes = await _postulanteRepository.ObtenerPostulantesPorProyectoAsync(idProyecto);
            return postulantes;
        }
    }
}