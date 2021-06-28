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
    
    public class PlanAccionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlanAccionRepository _planAccionRepository;
        private readonly IActividadRepository _actividadRepository;
        private readonly IPlazoAperturaRepository _plazoAperturaRepository;
        private readonly IMailServer _mailServer;
        public PlanAccionController(
            IUnitOfWork unitOfWork, 
            IPlanAccionRepository planAccionRepository, 
            IPlazoAperturaRepository plazoAperturaRepository,
            IActividadRepository actividadRepository,
            IMailServer mailServer
        )

        {
            _unitOfWork = unitOfWork;
            _planAccionRepository = planAccionRepository;
            _actividadRepository = actividadRepository;
            _plazoAperturaRepository = plazoAperturaRepository;
            _mailServer = mailServer;
        }
        [HttpPost]
        public IActionResult PostCrearPlanAccion(PlanAccionRequest request)
        {
            var service = new CrearPlanAccionService(_unitOfWork, _actividadRepository, _planAccionRepository, _plazoAperturaRepository, _mailServer);
            var response = service.Handle(request);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult ConsultarPlanAccion(int id)
        {
            var service = new ConsultarPlanAccionService(_planAccionRepository);
            var response = service.GetPlan(id);
            return Ok(response);
        }
        [HttpGet("GetByActividad/{actividadId}")]
        public IActionResult GetByActividad(int actividadId)
        {
            var service = new ConsultarPlanAccionService(_planAccionRepository);
            var response = service.GetByActividad(actividadId);
            return Ok(response);
        }
    }
}