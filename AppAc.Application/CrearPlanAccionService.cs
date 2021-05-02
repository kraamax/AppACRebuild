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
            var actividad = _actividadRepository.Find(request.ActividadId);
            if (actividad == null)
                return new PlanAccionResponse("Debe tener una actividad asignada para crear una plan de acciones");
            var planAccion = new PlanAccion();
            var errors = CanConvertToItemPlan(request.Items);
            if (errors.Any())
            {
                return new PlanAccionResponse(convertToString(errors));
            }
            var items = ConvertToItemPlan(request.Items);
            errors.AddRange(planAccion.CanDeliver(items,actividad));
            if (errors.Any())
            {
                return new PlanAccionResponse(convertToString(errors));
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
            if (actividad.Docente == null)
                return new PlanAccionResponse("No trajo docente");
            _emailServer.Send("Nueva plan registrado",$"Se registro el plan de acciones", actividad.Docente.Email);
            return new PlanAccionResponse(response);
        }
        private List<string> CanConvertToItemPlan(List<ItemPlanRequest> items)
        {
            var errors = new List<string>();
            foreach (var item in items)
            {
                var itemPlan = new ItemPlan();
                var accionPlaneada = new AccionPlaneada();
                errors.AddRange(accionPlaneada.CanDeliver(item.AccionPlaneada_Descripcion));
                var accionRealizada = new AccionRealizada();
                var evidencia = new Evidencia();
                errors.AddRange(evidencia.CanDeliver(item.AccionRealizada_evidencia_Ruta));
                accionRealizada.CanDeliver(item.AccionRealizada_Descripcion,evidencia);
                errors.AddRange(itemPlan.CanDeliver(accionPlaneada,accionRealizada));
            }
            return errors;
        }

        private List<ItemPlan> ConvertToItemPlan(List<ItemPlanRequest> items)
        {
            var itemsPlan = new List<ItemPlan>();
            foreach (var item in items)
            {
                var itemPlan = new ItemPlan();
                var accionPlaneada = new AccionPlaneada();
                accionPlaneada.Deliver(item.AccionPlaneada_Descripcion);
                var accionRealizada = new AccionRealizada();
                var evidencia = new Evidencia();
                evidencia.Deliver(item.AccionRealizada_evidencia_Ruta);
                accionRealizada.Deliver(item.AccionRealizada_Descripcion,evidencia);
                itemPlan.Deliver(accionPlaneada,accionRealizada);
                itemsPlan.Add(itemPlan);
            }
            return itemsPlan;
        }
        private string convertToString(List<string> errors)
        {
            var result = String.Join(", ", errors.ToArray());
            return result;
        }
        
    }
    public record PlanAccionRequest(int ActividadId, List<ItemPlanRequest> Items);
    public record PlanAccionResponse(string Message);
}
