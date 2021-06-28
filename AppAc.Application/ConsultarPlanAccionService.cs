using AppAC.Domain;
using AppAC.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using AppAC.Application;

namespace AppAc.Application
{
    public class ConsultarPlanAccionService
    {
        private readonly IPlanAccionRepository _planAccionRepository;

        public ConsultarPlanAccionService(
            IPlanAccionRepository planAccionRepository
        )
        {
            _planAccionRepository = planAccionRepository;
        }

        public PlanAccion GetPlan(int id)
        {
            return _planAccionRepository.Find(id);
        }

        public PlanAccion GetByActividad(int actividadId)
        {
            return _planAccionRepository.FindByActividad(actividadId);
        }
    }

}
