using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class ItemPlan
    {
        public int IdItem { get; set; }
        public AccionPlaneada AccionPlaneada { get; set; }
        public int PlanAccionesId { get; set; }
        public AccionRealizada AccionRealizada { get; set; }
    }
}
