using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductAsks
{
    public class InpGetListAsks
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue)]
        public int Take { get; set; } = 10;

        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; } // شناسه کاربر خواننده

        [Display(Name = "HasFullControl")]
        public bool HasFullControl { get; set; }
    }
}
