using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain.Base
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}
