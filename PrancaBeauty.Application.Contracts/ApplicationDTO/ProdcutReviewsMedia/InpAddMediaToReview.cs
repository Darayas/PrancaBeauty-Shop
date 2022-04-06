using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProdcutReviewsMedia
{
    public class InpAddMediaToReview
    {
        [Display(Name = "ProductReviewId")]
        [RequiredString]
        [GUID]
        public string ProductReviewId { get; set; }

        [Display(Name = "MediaIds")]
        [RequiredString]
        [GUID]
        public string MediaIds { get; set; }
    }
}
