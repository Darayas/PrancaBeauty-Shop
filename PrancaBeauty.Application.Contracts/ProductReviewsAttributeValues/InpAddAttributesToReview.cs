using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductReviewsAttributeValues
{
    public class InpAddAttributesToReview
    {
        [Display(Name = "ProductReviewId")]
        [RequiredString]
        [GUID]
        public string ProductReviewId { get; set; }

        public List<InpAddAttributesToReviewItem> Items { get; set; } = new List<InpAddAttributesToReviewItem>();
    }

    public class InpAddAttributesToReviewItem
    {
        [Display(Name = "AttributeId")]
        [RequiredString]
        [GUID]
        public string AttributeId { get; set; }

        [Display(Name = "Value")]
        [RequiredString]
        [NumRange(0, 5)]
        public int Value { get; set; }
    }
}
