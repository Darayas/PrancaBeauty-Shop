using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductReviewsAttributeValues
{
    public class InpGetAvgAttributesByReviewId
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
    }
}
