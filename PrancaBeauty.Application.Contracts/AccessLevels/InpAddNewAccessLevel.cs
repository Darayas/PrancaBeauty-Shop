using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class InpAddNewAccessLevel
    {
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}
