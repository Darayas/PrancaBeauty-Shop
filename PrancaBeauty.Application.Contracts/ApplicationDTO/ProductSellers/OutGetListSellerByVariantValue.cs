using Framework.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductSellers
{
    public class OutGetListSellerByVariantValue
    {
        public string VariantId { get; set; }
        public string SellerLogo { get; set; }
        public string SellerName { get; set; }
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده
        public string GarrantyName { get; set; }
        public double PercentSavePrice { get; set; }
        //public double Price { get; set; }
        //public double SellerPercentPrice { get; set; }
        public double MainPrice { get; set; }
        public double OldPrice { get; set; }
        public string CurrencySymbol { get; set; }
    }
}
