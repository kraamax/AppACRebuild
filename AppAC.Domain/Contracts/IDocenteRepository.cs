using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Domain.Contracts
{
    public interface IDocenteRepository: IGenericRepository<Docente>
    {
        Docente FindDocente(string identificacion);
    }
}
