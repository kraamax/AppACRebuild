using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAc.Application;
using AppAC.Application;
using AppAC.Domain;
using AppAC.Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAC.Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ActividadController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITipoActividadRepository _tipoActividadRepository;
        private readonly IActividadRepository _actividadRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMailServer _mailServer;
        public ActividadController(
            IUnitOfWork unitOfWork, 
            ITipoActividadRepository tipoActividadRepository, 
            IActividadRepository actividadRepository,
            IUsuarioRepository usuarioRepository,
            IMailServer mailServer
            
        )

        {
            _unitOfWork = unitOfWork;
            _tipoActividadRepository = tipoActividadRepository;
            _actividadRepository = actividadRepository;
            _usuarioRepository = usuarioRepository;
            _mailServer = mailServer;
        }
        [HttpPost]
        public IActionResult PostCrearActividad(ActividadRequest request)
        {
            var service = new AsignarActividadService(_unitOfWork, 
                                                        _actividadRepository,  
                                                        _usuarioRepository,
                                                        _tipoActividadRepository,
                                                        _mailServer);
            var response = service.Handle(request);
            return Ok(response);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var service = new ConsultarActividadService(_actividadRepository);
            var response = service.GetAll();
            return Ok(response);
        }
        [HttpGet("GetByDocente/{identificacion}")]
        public IActionResult GetByDocente(string identificacion)
        {
            var service = new ConsultarActividadService(_actividadRepository);
            var response = service.GetByDocente(identificacion);
            return Ok(response);
        }
        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var service = new ConsultarActividadService(_actividadRepository);
            var response = service.Get(id);
            return Ok(response);
        }
    }
}