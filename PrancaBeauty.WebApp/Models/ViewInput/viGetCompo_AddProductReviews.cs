using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viGetCompo_AddProductReviews
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
    }
}
