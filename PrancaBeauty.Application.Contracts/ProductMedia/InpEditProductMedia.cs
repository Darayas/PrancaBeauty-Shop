using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductMedia
{
    public class InpEditProductMedia
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "MediaIds")]
        [RequiredString]
        [GUID]
        public string MediaIds { get; set; }
    }
}
