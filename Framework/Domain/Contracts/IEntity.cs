using System;

namespace Framework.Domain.Contracts
{
    public interface IEntity<T> : IBaseEntity<T>
    {

    }

    public interface IEntity : IEntity<Guid>
    {

    }
}
