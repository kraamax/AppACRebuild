using AppAC.Domain;
using AppAC.Domain.Contracts;
using AppAC.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Infrastructure.Data
{
    public class PlanAccionRepository : GenericRepository<PlanAccion>, IPlanAccionRepository
    {
        public PlanAccionRepository(IDbContext context) : base(context)
        {
        }
    }
}
