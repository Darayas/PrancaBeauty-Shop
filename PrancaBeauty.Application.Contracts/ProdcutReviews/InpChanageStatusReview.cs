using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProdcutReviews
{
    public class InpChanageStatusReview
    {
        [Display(Name = "AuthorUserId")]
        [RequiredString]
        [GUID]
        public string AuthorUserId { get; set; }
        
        [Display(Name = "ReviewId")]
        [RequiredString]
        [GUID]
        public string ReviewId { get; set; }
    }
}
