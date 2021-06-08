using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Address
{
    public class GetAddressByUserIdForManage
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public int CountBills { get; set; }
        public DateTime Date { get; set; }
    }
}
