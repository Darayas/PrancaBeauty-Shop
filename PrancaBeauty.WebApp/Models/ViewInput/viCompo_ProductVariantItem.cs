using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ProductVariantItem
    {
        [Display(Name ="ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductVariantId { get; set; }
    }
}
