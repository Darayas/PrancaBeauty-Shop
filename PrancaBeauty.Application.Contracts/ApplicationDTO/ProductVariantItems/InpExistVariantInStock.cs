using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems
{
    public class InpExistVariantInStock
    {
        [Display(Name = "VariantItemId")]
        [RequiredString]
        [GUID]
        public string VariantItemId { get; set; }

        [Display(Name = "CountToCheck")]
        public int CountToCheck { get; set; }
    }
}