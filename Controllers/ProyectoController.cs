using Microsoft.AspNetCore.Mvc;
using MyChamba.Data.UnitofWork;
using MyChamba.Models;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Controllers
{
    /// <summary>
    /// Controlador para gestionar operaciones relacionadas con proyectos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor del controlador de proyectos.
        /// </summary>
        /// <param name="unitOfWork">Instancia del patrón UnitOfWork</param>
        public ProyectosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        

    }
}
