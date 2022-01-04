using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductSellers
{
    public class vmGetAllSellerForManageByProductId
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirm  { get; set; }
        public string VariantId { get; set; }
    }
}
