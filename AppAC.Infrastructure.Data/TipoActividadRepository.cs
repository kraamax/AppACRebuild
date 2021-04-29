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
    public class TipoActividadRepository : GenericRepository<TipoActividad>, ITipoActividadRepository
    {
        public TipoActividadRepository(IDbContext context) : base(context)
        {
        }
    }
}
