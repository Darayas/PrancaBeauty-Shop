using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProdcutReviews
{
    public class InpRemoveReview
    {
        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "ReviewId")]
        [RequiredString]
        [GUID]
        public string ReviewId { get; set; }
    }
}
