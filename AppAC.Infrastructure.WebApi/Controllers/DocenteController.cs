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
    
    public class DocenteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocenteRepository _docenteRepository;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMailServer _mailServer;
        public DocenteController(
            IUnitOfWork unitOfWork, 
            IDocenteRepository docenteRepository, 
            IDepartamentoRepository departamentoRepository,
            IMailServer mailServer
        )

        {
            _unitOfWork = unitOfWork;
            _docenteRepository = docenteRepository;
            _departamentoRepository = departamentoRepository;
            _mailServer = mailServer;
        }
        [HttpPost]
        public IActionResult PostCrearDocente(DocenteRequest request)
        {
            var service = new CrearDocenteService(_unitOfWork, _departamentoRepository, _docenteRepository, _mailServer);
            var response = service.Handle(request);
            return Ok(response);
        }
        [HttpGet("GetAll")]
        public IActionResult GetALl()
        {
            var service = new ConsultarDocenteService(_docenteRepository);
            var response = service.GetAll();
            return Ok(response);
        }
        [HttpGet("GetByIdentificacion/{identificacion}")]
        public IActionResult GetByIdentificacion(string identificacion)
        {
            var service = new ConsultarDocenteService(_docenteRepository);
            var response = service.GetByIdentificacion(identificacion);
            return Ok(response);
        }
        [HttpGet("GetByDpto/{dptoId}")]
        public IActionResult GetByIdentificacion(int dptoId)
        {
            var service = new ConsultarDocenteService(_docenteRepository);
            var response = service.GetByDepartamento(dptoId);
            return Ok(response);
        }
    }
}