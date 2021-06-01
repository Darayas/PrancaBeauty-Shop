using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Infrastructure
{
    public interface ILocalizer
    {
        public string this[string Name] { get; }
        public string this[string Name, params object[] argumets] { get; }
    }
}