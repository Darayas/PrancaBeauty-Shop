using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Address
{
    public class OutGetAddressByUserIdForManage
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public int CountBills { get; set; }
        public DateTime Date { get; set; }
    }
}
