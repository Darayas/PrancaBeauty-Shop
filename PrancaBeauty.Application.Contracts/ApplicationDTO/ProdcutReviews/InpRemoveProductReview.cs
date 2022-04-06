using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProdcutReviews
{
    public class InpRemoveProductReview
    {
        [Display(Name = "ProductReviewId")]
        [RequiredString]
        [GUID]
        public string ProductReviewId { get; set; }
    }
}
