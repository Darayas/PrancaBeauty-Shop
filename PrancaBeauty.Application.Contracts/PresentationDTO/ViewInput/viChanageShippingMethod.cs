using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viChanageShippingMethod
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
    }
}
