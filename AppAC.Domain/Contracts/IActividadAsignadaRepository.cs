using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain.Contracts
{
    public interface IActividadAsignadaRepository
    {
       Actividad Find(int id);
    }
}
