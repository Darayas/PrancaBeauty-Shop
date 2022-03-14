using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProdcutReviews
{
    public class InpGetReviewsForProductDetails
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; } // شناسه کاربر خواننده

        [Display(Name = "HasFullControl")]
        public bool HasFullControl { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Take { get; set; } = 10;

        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Page { get; set; } = 1;
    }
}
