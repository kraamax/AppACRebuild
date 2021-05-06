using AppAC.Domain;
using AppAC.Domain.Contracts;
using AppAC.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppAC.Infrastructure.Data
{
    public class PlazoAperturaRepository : GenericRepository<PlazoApertura>, IPlazoAperturaRepository
    {
        public PlazoAperturaRepository(IDbContext context) : base(context)
        {
        }

        public PlazoApertura GetCurrentPlazoByCreador(string idCreador)
        {
            return _db.Set<PlazoApertura>().Include(p => p.Creador)
                .Where(p => p.Creador.Identificacion == idCreador)
                .FirstOrDefault(p => p.Activo == true);
        }
    }
}
