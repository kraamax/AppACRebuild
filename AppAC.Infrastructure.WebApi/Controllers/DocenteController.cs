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
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMailServer _mailServer;
        public DocenteController(
            IUnitOfWork unitOfWork, 
            IUsuarioRepository usuarioRepository, 
            IDepartamentoRepository departamentoRepository,
            IMailServer mailServer
        )

        {
            _unitOfWork = unitOfWork;
            _usuarioRepository = usuarioRepository;
            _departamentoRepository = departamentoRepository;
            _mailServer = mailServer;
        }
        [HttpPost]
        public IActionResult PostCrearDocente(DocenteRequest request)
        {
            var service = new CrearDocenteService(_unitOfWork, _departamentoRepository, _usuarioRepository, _mailServer);
            var response = service.Handle(request);
            return Ok(response);
        }
    }
}