using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems
{
    public class InpGetProductPriceByVariantItemId
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "ProductVariantValue")]
        [RequiredString]
        [MaxLengthString(100)]
        public string ProductVariantValue { get; set; }
    }
}
