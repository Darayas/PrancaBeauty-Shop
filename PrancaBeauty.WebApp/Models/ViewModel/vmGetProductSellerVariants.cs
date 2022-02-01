using Framework.Application.Enums;

namespace PrancaBeauty.WebApp.Models.ViewModel
{
    public class vmGetProductSellerVariants
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public double Percent { get; set; }
        public string GuaranteeTitle { get; set; }
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده
        public bool IsEnable { get; set; }
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده
        public int CountInStock { get; set; }
        public bool IsConfirm { get; set; }
    }
}
