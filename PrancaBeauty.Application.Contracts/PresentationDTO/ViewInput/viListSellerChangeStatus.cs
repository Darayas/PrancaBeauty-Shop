using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viListSellerChangeStatus
    {
        [Display(Name = "ProductSellerId")]
        [RequiredString]
        [GUID]
        public string ProductSellerId { get; set; }

        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
    }
}
