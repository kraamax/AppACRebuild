using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
