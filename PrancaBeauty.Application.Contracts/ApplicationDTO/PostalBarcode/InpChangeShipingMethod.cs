using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.PostalBarcode
{
    public class InpChangeShipingMethod
    {
        [Display(Name = "GroupId")]
        [RequiredString]
        [GUID]
        public string GroupId { get; set; }
        [Display(Name = "BillId")]
        [RequiredString]
        [GUID]
        public string BillId { get; set; }

        [Display(Name = "ShippingMethodId")]
        [RequiredString]
        [GUID]
        public string ShippingMethodId { get; set; }

        [Display(Name = "BuyerUserId")]
        [RequiredString]
        [GUID]
        public string BuyerUserId { get; set; }
    }
}
