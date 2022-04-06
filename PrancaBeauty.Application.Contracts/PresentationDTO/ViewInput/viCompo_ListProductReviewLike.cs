using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_ListProductReviewLike
    {
        [Display(Name = "ReviewId")]
        [RequiredString]
        [GUID]
        public string ReviewId { get; set; }
    }
}
