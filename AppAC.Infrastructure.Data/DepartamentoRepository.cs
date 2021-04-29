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
    public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(IDbContext context) : base(context)
        {
        }
    }
}
