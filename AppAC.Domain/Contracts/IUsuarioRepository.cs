using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAC.Domain.Contracts
{
    public interface IUsuarioRepository: IGenericRepository<Usuario>
    {
        Docente FindDocente(string identificacion);
        JefeDpto FindJefeDpto(string identificacion);
        
    }
}
