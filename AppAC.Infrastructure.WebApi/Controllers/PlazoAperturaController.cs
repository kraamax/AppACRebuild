using AppAC.Application;
using AppAC.Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAC.Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlazoAperturaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlazoAperturaRepository _plazoAperturaRepository;
        private readonly IJefeDptoRepository _jefeDptoRepository;

        private readonly IMailServer _mailServer;
        public PlazoAperturaController(
            IUnitOfWork unitOfWork, 
            IPlazoAperturaRepository plazoAperturaRepository, 
            IMailServer mailServer,
            IJefeDptoRepository jefeDptoRepository
            )

        {
            _unitOfWork = unitOfWork;
            _plazoAperturaRepository = plazoAperturaRepository;
            _mailServer = mailServer;
            _jefeDptoRepository = jefeDptoRepository;
        }
        [HttpPost]
        public string PostCrearPlazoApertura(PlazoAperturaRequest request)
        {
            var service = new PlazoAperturaService(_unitOfWork ,_plazoAperturaRepository, _mailServer,_jefeDptoRepository);
            var response = service.CrearPlazoApertura(request);
            return response;
        }
    }
}
