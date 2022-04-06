using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems
{
    public class InpRemoveVariantsFromProduct
    {
        [Display(Name = "CountryId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "CountryId")]
        [GUID]
        public string ProductSellerId { get; set; }


    }
}
