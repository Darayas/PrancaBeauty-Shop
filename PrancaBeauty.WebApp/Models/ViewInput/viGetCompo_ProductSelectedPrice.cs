using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viGetCompo_ProductSelectedPrice
    {
        [Display(Name = "ProductVariantItemId")]
        [RequiredString]
        [GUID]
        public string ProductVariantItemId { get; set; }
    }
}
