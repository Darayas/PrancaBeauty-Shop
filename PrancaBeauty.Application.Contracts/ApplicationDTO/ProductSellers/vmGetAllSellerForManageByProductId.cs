using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductSellers
{
    public class vmGetAllSellerForManageByProductId
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string SellerName { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirm  { get; set; }
        public bool HasUnConfermVariants { get; set; }
    }
}
