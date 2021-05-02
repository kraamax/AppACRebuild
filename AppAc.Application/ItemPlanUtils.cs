using System.Collections.Generic;
using AppAc.Application;
using AppAC.Domain;

namespace AppAC.Application
{
    public static class ItemPlanUtils
    {
        public static ItemPlan ConvertToItemPlan(ItemPlanRequest request)
        {
            var item = new ItemPlan();
            var accionPlaneada = new AccionPlaneada();
            accionPlaneada.Deliver(request.AccionPlaneada_Descripcion);
            var accionRealizada = new AccionRealizada();
            var evidencia = new Evidencia();
            evidencia.Deliver(request.AccionRealizada_evidencia_Ruta);
            accionRealizada.Deliver(request.AccionRealizada_Descripcion,evidencia);
            item.Deliver(accionPlaneada,accionRealizada,request.PlanId);
            return item;
        }
        public static List<string> CanConvertToItemPlan(ItemPlanRequest request)
        {
            var errors = new List<string>();
            var item = new ItemPlan();
            var accionPlaneada = new AccionPlaneada();
            errors.AddRange(accionPlaneada.CanDeliver(request.AccionPlaneada_Descripcion));
            var accionRealizada = new AccionRealizada();
            var evidencia = new Evidencia();
            errors.AddRange(evidencia.CanDeliver(request.AccionRealizada_evidencia_Ruta));
            errors.AddRange(accionRealizada.CanDeliver(request.AccionRealizada_Descripcion,evidencia));
            errors.AddRange(item.CanDeliver(accionPlaneada,accionRealizada));
            return errors;
        }
        
    }
}