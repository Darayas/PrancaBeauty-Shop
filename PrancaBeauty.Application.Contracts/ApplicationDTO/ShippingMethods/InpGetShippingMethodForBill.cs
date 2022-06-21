using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShippingMethods
{
    public class InpGetShippingMethodForBill
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

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
