using AppAC.Domain;
using AppAC.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using AppAC.Application;

namespace AppAc.Application
{
    public class CrearPlanAccionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActividadRepository _actividadRepository;
        private readonly IPlanAccionRepository _planAccionRepository;
        private readonly IMailServer _emailServer;

        public CrearPlanAccionService(
           IUnitOfWork unitOfWork,
           IActividadRepository actividadRepository,
           IPlanAccionRepository planAccionRepository,
           IMailServer emailServer
       )
        {
            _unitOfWork = unitOfWork;
            _actividadRepository = actividadRepository;
            _planAccionRepository = planAccionRepository;
            _emailServer = emailServer;
        }
        public PlanAccionResponse Handle(PlanAccionRequest request)
        {
            var planAccion = _planAccionRepository.FindByActividad(request.ActividadId);
            if (planAccion != null)
                return new PlanAccionResponse("Ya existe un plan de acción para esta actividad",planAccion);
            
            var actividad = _actividadRepository.Find(request.ActividadId);
            if (actividad == null)
                return new PlanAccionResponse("Debe tener una actividad asignada para crear una plan de acciones",planAccion);
            
            planAccion = new PlanAccion();
            var errors = canConvertToItemPlanList(request.Items);
            if (errors.Any())
            {
                return new PlanAccionResponse(StringUtils.ToString(errors),planAccion);
            }
            var items = ConvertToItemPlanList(request.Items);
            errors.AddRange(planAccion.CanDeliver(items,actividad));
            if (errors.Any())
            {
                return new PlanAccionResponse(StringUtils.ToString(errors),planAccion);
            }
            planAccion.Deliver(items,actividad);
            var response = "";
            try
            {
                _planAccionRepository.Add(planAccion);
                response = "Plan de accion registrado correctamente";
            }
            catch (Exception e)
            {
                response = "No se pudo registrar";
            }
            _unitOfWork.Commit();
            if (actividad.Responsable == null)
                return new PlanAccionResponse("No trajo docente",planAccion);
            _emailServer.Send("Nueva plan registrado",$"Se registro el plan de acciones", actividad.Responsable.Email);
            return new PlanAccionResponse(response,planAccion);
        }
        
        private List<string> canConvertToItemPlanList(List<ItemPlanRequest> items)
        {
            var errors = new List<string>();
            foreach (var item in items)
            {
                errors.AddRange(ItemPlanUtils.CanConvertToItemPlan(item));
            }
            return errors;
        }

        private List<ItemPlan> ConvertToItemPlanList(List<ItemPlanRequest> items)
        {
            var itemsPlan = new List<ItemPlan>();
            foreach (var item in items)
            {
                var itemPlan=ItemPlanUtils.ConvertToItemPlan(item);
                itemsPlan.Add(itemPlan);
            }
            return itemsPlan;
        }
        
   
        
    }
    public record PlanAccionRequest(int ActividadId, List<ItemPlanRequest> Items);
    public record PlanAccionResponse(string Message, PlanAccion PlanAccion);
}
