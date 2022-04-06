using Framework.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmGetProductSellerVariants
    {
        public string Id { get; set; }

        [Display(Name ="Title")]
        public string Title { get; set; }

        [Display(Name = "Value")]
        public string Value { get; set; }

        [Display(Name = "Percent")]
        public double Percent { get; set; }

        [Display(Name = "GuaranteeTitle")]
        public string GuaranteeTitle { get; set; }

        [Display(Name = "SendBy")]
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        [Display(Name = "SendFrom")]
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده

        [Display(Name = "Inventory")]
        public int CountInStock { get; set; }

        [Display(Name = "IsConfirm")]
        public bool IsConfirm { get; set; }
    }
}
