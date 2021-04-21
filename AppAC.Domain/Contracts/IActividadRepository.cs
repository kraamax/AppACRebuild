using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain.Contracts
{
    public interface IActividadRepository
    {
       Actividad Find(int id);
    }
}
