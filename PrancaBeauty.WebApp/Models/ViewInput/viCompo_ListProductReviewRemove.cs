using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ListProductReviewRemove
    {
        [Display(Name = "ReviewId")]
        [RequiredString]
        [GUID]
        public string ReviewId { get; set; }
    }
}
