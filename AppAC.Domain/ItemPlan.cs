using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAC.Domain
{
    public class ItemPlan: Entity<int>,IAggregateRoot
    {
        public AccionPlaneada AccionPlaneada { get; private set; }
        public AccionRealizada AccionRealizada { get; private set; }
        public int PlanAccionId { get; private set; }
        public IReadOnlyList<string> CanDeliver(AccionPlaneada accionPlaneada, AccionRealizada accionRealizada) {
                    var errors = new List<string>();
                    if (accionPlaneada==null)
                        errors.Add("Debe tener una accion planeada");
                    
                    if(accionRealizada==null)
                        errors.Add("Debe tener una accion realizada");
                    return errors;
        }
        public void Deliver(AccionPlaneada accionPlaneada, AccionRealizada accionRealizada, int planAccionId)
        {
            if (CanDeliver(accionPlaneada, accionRealizada).Any())
                throw new InvalidOperationException();
            AccionPlaneada = accionPlaneada;
            AccionRealizada = accionRealizada;
            PlanAccionId = planAccionId;
        }
    }
}
