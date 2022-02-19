using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ProductReviewAttributes
    {
        public string AttributeId { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Value")]
        public string Value { get; set; }
    }
}
