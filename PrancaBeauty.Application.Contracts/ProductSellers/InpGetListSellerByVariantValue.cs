using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductSellers
{
    public class InpGetListSellerByVariantValue
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "VariaintValue")]
        [RequiredString]
        [MaxLengthString(100)]
        public string VariaintValue { get; set; }
    }
}
