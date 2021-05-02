using System.Collections.Generic;
using AppAC.Domain;

namespace AppAC.Infrastructure.Data.ObjectMother
{
    public static class PlanAccionMother
    {
        public static PlanAccion CreatePlanAccion()
        {
            var actividad = ActividadMother.CreateActividad();
            var accionPlaneada = new AccionPlaneada();
            accionPlaneada.Deliver("Se describe lo planeado");
            var accionRealizada = new AccionRealizada();
            var evidencia = new Evidencia();
            evidencia.Deliver("loquesea/dir");
            accionRealizada.Deliver("Se describe lo realizado",evidencia);
            var itemPlan = new ItemPlan();
            itemPlan.Deliver(accionPlaneada,accionRealizada,0);
            var items = new List<ItemPlan>();
            items.Add(itemPlan);
            var planAccion = new PlanAccion(items,actividad);
            return planAccion;
        }
    }
}