using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_AddProductReviews
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "TextReview")]
        [RequiredString]
        [MaxLengthString(5000)]
        public string Text { get; set; }

        [Display(Name = "Advantages")]
        [MaxLengthString(2000)]
        public string Advantages { get; set; } // نقاط قوت

        [Display(Name = "DisAdvantages")]
        [MaxLengthString(2000)]
        public string DisAdvantages { get; set; } // نقاط ضعف

        [Display(Name = "Rating")]
        public int CountStar { get; set; }

        [Display(Name = "MediaIds")]
        public string MediaIds { get; set; }

        public List<viCompo_AddProductReviewsAttributes> LstAttribute { get; set; }

    }

    public class viCompo_AddProductReviewsAttributes
    {
        [Display(Name = "AttributeId")]
        [RequiredString]
        [GUID]
        public string AttributeId { get; set; }

        [Display(Name = "Value")]
        [RequiredString]
        public string Value { get; set; }
    }
}
