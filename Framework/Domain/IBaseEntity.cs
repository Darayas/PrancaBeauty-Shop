using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}
