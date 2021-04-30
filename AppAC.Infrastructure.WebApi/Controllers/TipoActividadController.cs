using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAC.Application;
using AppAC.Domain;
using AppAC.Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAC.Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TipoActividadController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoActividadRepository _tipoActividadRepository;
        private readonly IMailServer _mailServer;
        public TipoActividadController(
            IUnitOfWork unitOfWork, 
            ITipoActividadRepository tipoActividadRepository, 
            IMailServer mailServer
        )

        {
            _unitOfWork = unitOfWork;
            _tipoActividadRepository = tipoActividadRepository;
            _mailServer = mailServer;
        }
        [HttpPost]
        public IActionResult PostCrearTipoActividad(TipoActividadRequest request)
        {
            var service = new TipoActividadService(_unitOfWork, _tipoActividadRepository, _mailServer);
            var response = service.CrearTipoActividad(request);
            return Ok(response);
        }
    }
}