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
    
    public class JefeDptoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJefeDptoRepository _jefeDptoRepository;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMailServer _mailServer;
        public JefeDptoController(
            IUnitOfWork unitOfWork, 
            IJefeDptoRepository jefeDptoRepository, 
            IDepartamentoRepository departamentoRepository,
            IMailServer mailServer
        )

        {
            _unitOfWork = unitOfWork;
            _jefeDptoRepository = jefeDptoRepository;
            _departamentoRepository = departamentoRepository;
            _mailServer = mailServer;
        }
        [HttpPost]
        public IActionResult PostCrearJefeDpto(JefeDptoRequest request)
        {
            var service = new CrearJefeDptoService(_unitOfWork, _departamentoRepository, _jefeDptoRepository, _mailServer);
            var response = service.Handle(request);
            return Ok(response);
        }
        
    }
}