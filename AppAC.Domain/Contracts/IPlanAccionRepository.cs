using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Domain.Contracts
{
    public interface IPlanAccionRepository : IGenericRepository<PlanAccion>
    {
        PlanAccion FindByActividad(int actividadId);
        PlanAccion Find(int id);
    }
}
