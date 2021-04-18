using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    class PlanAccion
    {
        public int IdPlanAcciones { get; set; }
        public List<ItemPlan> Items { get; set; }
        public DateTime Fecha { get; set; }
        public int ActividadId { get; set; }
        public TipoActividad Actividad { get; set; }
    }
}
