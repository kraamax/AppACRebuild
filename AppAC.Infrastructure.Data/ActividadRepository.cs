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
    public class ActividadRepository : GenericRepository<Actividad>, IActividadRepository
    {
        public ActividadRepository(IDbContext context) : base(context)
        {
        }

        public Actividad Find(int id)
        {
            return _db.Set<Actividad>()
                .Include(c=>c.Responsable)
                .Include(c=>c.Asignador)
                .Include(c=>c.TipoActividad)
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Actividad> FindByDocente(string identificacion)
        {
            return _db.Set<Actividad>()
                .Include(c=>c.TipoActividad)
                .Where(c => c.Responsable.Identificacion == identificacion);
        }
    }
}
