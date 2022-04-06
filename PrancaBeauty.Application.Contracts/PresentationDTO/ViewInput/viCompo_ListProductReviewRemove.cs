using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_ListProductReviewRemove
    {
        [Display(Name = "ReviewId")]
        [RequiredString]
        [GUID]
        public string ReviewId { get; set; }
    }
}
