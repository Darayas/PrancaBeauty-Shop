using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viGetProductSellerDetails
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "ProductSellerId")]
        [RequiredString]
        [GUID]
        public string ProductSellerId { get; set; }
    }
}
