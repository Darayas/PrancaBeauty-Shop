using Framework.Application.Enums;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompo_ProductSellers
    {
        public string SellerId { get; set; }
        public string VariantItemId { get; set; }
        public string SellerLogo { get; set; }
        public string SellerName { get; set; }
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده
        public string GarrantyName { get; set; }
        public string PercentSavePrice { get; set; }
        //public double Price { get; set; }
        //public double SellerPercentPrice { get; set; }
        public string MainPrice { get; set; }
        public string OldPrice { get; set; }
        public string CurrencySymbol { get; set; }
    }
}
