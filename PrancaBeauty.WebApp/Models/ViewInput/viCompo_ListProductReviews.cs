using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ListProductReviews
    {
        [Display]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
    }
}
