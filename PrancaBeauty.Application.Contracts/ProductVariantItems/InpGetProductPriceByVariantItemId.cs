using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductVariantItems
{
    public class InpGetProductPriceByVariantItemId
    {
        [Display(Name = "ProductVariantItemId")]
        [RequiredString]
        [GUID]
        public string ProductVariantItemId { get; set; }
    }
}
