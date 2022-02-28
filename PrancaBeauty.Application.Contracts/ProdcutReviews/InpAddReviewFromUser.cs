using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.ProdcutReviews
{
    public class InpAddReviewFromUser
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "AuthorUserId")]
        [RequiredString]
        [GUID]
        public string AuthorUserId { get; set; }

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
        [NumRange(0, 5)]
        public int CountStar { get; set; }

        [Display(Name = "IpAddress")]
        [RequiredString]
        public string IpAddress { get; set; }

        [Display(Name = "MediaIds")]
        public string MediaIds { get; set; }

        public List<InpAddReviewFromUserAttributes> LstAttribute { get; set; }

    }

    public class InpAddReviewFromUserAttributes
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
