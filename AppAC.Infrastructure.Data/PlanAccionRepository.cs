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
    public class PlanAccionRepository : GenericRepository<PlanAccion>, IPlanAccionRepository
    {
        public PlanAccionRepository(IDbContext context) : base(context)
        {
        }

        public PlanAccion FindByActividad(int actividadId)
        {
            return _db.Set<PlanAccion>()
                .Include(c => c.Actividad).ThenInclude(c=>c.Responsable)
                .Include(c=>c.Items).ThenInclude(c=>c.AccionPlaneada)
                .Include(c=>c.Items).ThenInclude(c=>c.AccionRealizada)
                .ThenInclude(a=>a.Evidencia)
                .FirstOrDefault(c => c.Actividad.Id == actividadId);
        }

        public PlanAccion Find(int id)
        {
            return _db.Set<PlanAccion>()
                .Include(c=>c.Actividad).ThenInclude(a=>a.Asignador)
                .Include(c=>c.Items).ThenInclude(c=>c.AccionPlaneada)
                .Include(c=>c.Items).ThenInclude(c=>c.AccionRealizada)
                .ThenInclude(a=>a.Evidencia)
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
