using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAC.Domain
{
    public class PlanAccion: Entity<int>, IAggregateRoot
    {
        public PlanAccion()
        {
        }

        public PlanAccion(List<ItemPlan> items, Actividad actividad)
        {
            Items = items;
            Fecha = DateTime.Now;
            Actividad = actividad;
        }

        public List<ItemPlan> Items { get; private set; }

        public DateTime Fecha { get; private set; }

        public Actividad Actividad { get; private set; }
        
        public IReadOnlyList<string> CanDeliver(List<ItemPlan> items, Actividad actividad) {
            var errors = new List<string>();
            if (items==null)
                errors.Add("Debe tener acciones");
                    
            if(actividad==null)
                errors.Add("Debe tener una actividad");
            return errors;
        }
        public void Deliver(List<ItemPlan> items, Actividad actividad)
        {
            if (CanDeliver(items, actividad).Any())
                throw new InvalidOperationException();
            Items = items;
            Actividad = actividad;
            Fecha=DateTime.Now;
        }
    }
}
