using Framework.Common.DataAnnotations.Numbers.All;
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

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Take { get; set; } = 10;

        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Page { get; set; } = 1;
    }
}
