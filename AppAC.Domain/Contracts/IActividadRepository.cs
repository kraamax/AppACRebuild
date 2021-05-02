using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain.Contracts
{
    public interface IActividadRepository:IGenericRepository<Actividad>
    {
        Actividad Find(int id);

    }
}
