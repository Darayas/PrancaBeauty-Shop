using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductReviewLikes
{
    public class InpLikeReview
    {
        [Display(Name = "ReviewId")]
        [RequiredString]
        [GUID]
        public string ReviewId { get; set; }

        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }
    }
}
