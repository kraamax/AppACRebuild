using AppAC.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Application.Test.Dobles
{
    class UnitOfWorkFake : IUnitOfWork
    {
        public void Commit()
        {
            Console.WriteLine("Se confirman cambios en la base de datos");
        }
    }
}
