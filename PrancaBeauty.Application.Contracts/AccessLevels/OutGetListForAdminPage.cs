using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class OutGetListForAdminPage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CountUser { get; set; }
    }
}
