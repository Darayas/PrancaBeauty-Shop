using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductVariantItems
{
    public class InpGetAllProductVariantsForProductDetails
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "DefaultVariantId")]
        [RequiredString]
        [GUID]
        public string DefaultVariantId { get; set; }
    }
}
