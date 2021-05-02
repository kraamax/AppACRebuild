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
            var errors = planAccion.CanDeliver(request.Items,actividad);
            if (errors.Any())
            {
                var result = String.Join(", ", errors.ToArray());
                return new PlanAccionResponse(result);
            }
            planAccion.Deliver(request.Items,actividad);
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
            _emailServer.Send("Nueva plan registrado",$"Se registro el plan de acciones", actividad.Docente.Email);
            return new PlanAccionResponse(response);
        }
    }
    public record PlanAccionRequest(int ActividadId, List<ItemPlan> Items);
    public record PlanAccionResponse(string Message);
}
