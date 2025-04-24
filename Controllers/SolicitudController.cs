using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MyChamba.Data.UnitofWork;
using MyChamba.DTOs.Solicitud;
using MyChamba.Models;
using Microsoft.EntityFrameworkCore;

namespace MyChamba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SolicitudController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       
    }
}
