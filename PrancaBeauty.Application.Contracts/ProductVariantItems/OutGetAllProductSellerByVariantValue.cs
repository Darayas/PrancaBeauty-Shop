using Framework.Application.Enums;

namespace PrancaBeauty.Application.Contracts.ProductVariantItems
{
    public class OutGetAllProductSellerByVariantValue
    {
        public string VariantId { get; set; }
        public string SellerLogo { get; set; }
        public string SellerName { get; set; }
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده
        public string GarrantyName { get; set; }
        public double PercentSavePrice { get; set; }
        public double Price { get; set; }
        public double SellerPercentPrice { get; set; }
        public double MainPrice { get { return Price; }  }
        public double OldPrice { get { return Price; }  }
        public string CurrencySymbol { get; set; }
    }
}
