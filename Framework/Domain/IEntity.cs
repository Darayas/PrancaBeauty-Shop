using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    public interface IEntity<T> : IBaseEntity<T>
    {

    }

    public interface IEntity : IEntity<Guid>
    {

    }
}
