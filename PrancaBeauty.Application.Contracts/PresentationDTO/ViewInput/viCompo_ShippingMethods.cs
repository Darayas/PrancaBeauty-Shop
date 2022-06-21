using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_ShippingMethods
    {
        [Display(Name = "ShippingMethodId")]
        [GUID]
        public string ShippingMethodId { get; set; }

        [Display(Name = "BuyerAddressId")]
        [RequiredString]
        [GUID]
        public string BuyerAddressId { get; set; }

        [Display(Name = "SellerAddressId")]
        [RequiredString]
        [GUID]
        public string SellerAddressId { get; set; }
    }
}
