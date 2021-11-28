using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductVariantItems
{
    public class InpRemoveVariantsFromProduct
    {
        [Display(Name = "CountryId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

    }
}
