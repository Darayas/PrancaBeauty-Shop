using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_ShippingMethods
    {
        [Display(Name = "BillId")]
        [RequiredString]
        [GUID]
        public string BillId { get; set; }

        [Display(Name = "GroupId")]
        [RequiredString]
        [GUID]
        public string GroupId { get; set; }

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

        public bool IsBuyer { get; set; }

        public bool IsPayed { get; set; }
    }
}
