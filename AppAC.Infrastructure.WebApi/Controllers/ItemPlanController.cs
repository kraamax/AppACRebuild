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
    
    public class ItemPlanController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlanAccionRepository _planAccionRepository;
        private readonly IItemPlanRepository _itemPlanRepository;
        private readonly IPlazoAperturaRepository _plazoAperturaRepository;
        public ItemPlanController(
            IUnitOfWork unitOfWork, 
            IPlanAccionRepository planAccionRepository, 
            IItemPlanRepository itemPlanRepository,
            IPlazoAperturaRepository plazoAperturaRepository
        )

        {
            _unitOfWork = unitOfWork;
            _planAccionRepository = planAccionRepository;
            _itemPlanRepository = itemPlanRepository;
            _plazoAperturaRepository = plazoAperturaRepository;
        }
        [HttpPost]
        public IActionResult PostCrearItemPlan(ItemPlanRequest request)
        {
            var service = new ItemPlanService(_unitOfWork, _planAccionRepository, _itemPlanRepository,_plazoAperturaRepository);
            var response = service.RegistrarItem(request);
            return Ok(response);
        }
        [HttpDelete]
        public IActionResult EliminarItemPlan(int id)
        {
            var service = new ItemPlanService(_unitOfWork, _planAccionRepository, _itemPlanRepository,_plazoAperturaRepository);
            var response = service.EliminarItem(id);
            return Ok(response);
        }
        [HttpPut]
        public IActionResult ModificarItemPlan(ItemPlanUpdateRequest request)
        {
            var service = new ItemPlanService(_unitOfWork, _planAccionRepository, _itemPlanRepository,_plazoAperturaRepository);
            var response = service.ModificarItem(request);
            return Ok(response);
        }
    }
}