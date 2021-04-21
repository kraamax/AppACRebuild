using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain.Base
{
    public abstract class Entity<T> : BaseEntity,IEntity<T>
    {
        public virtual T Id { get; }
    }
}
