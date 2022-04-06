using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Sellers
{
    public class OutGetSummaryBySellerId
    {
        public string Id { get; set; }
        public string FulUserName { get; set; }
        public string SellerTitle { get; set; }
        public DateTime DateTime { get; set; }
        public string SellerLogoUrl { get; set; }
        public bool IsSellerConfimed { get; set; }
    }
}
