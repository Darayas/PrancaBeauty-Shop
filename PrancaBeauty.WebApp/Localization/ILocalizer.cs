using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Localization
{
    public interface ILocalizer
    {
        public string this[string Name] { get; }
        public string this[string Name, params object[] argumets] { get; }
    }
}