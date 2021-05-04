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
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDbContext context) : base(context)
        {
        }

        public Docente FindDocente(string identificacion)
        {
           return _db.Set<Docente>().Include(d => d.Departamento)
               .FirstOrDefault(d => d.Identificacion == identificacion);
        }

        public JefeDpto FindJefeDpto(string identificacion)
        {
            return _db.Set<JefeDpto>().Include(d => d.Departamento)
                           .FirstOrDefault(d => d.Identificacion == identificacion);
        }
    }
}
