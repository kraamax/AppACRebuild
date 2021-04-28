using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class PlanAccion: Entity<int>, IAggregateRoot
    {
        public List<ItemPlan> Items { get; set; }
        public DateTime Fecha { get; set; }
        public int ActividadId { get; set; }
        public TipoActividad Actividad { get; set; }
    }
}
