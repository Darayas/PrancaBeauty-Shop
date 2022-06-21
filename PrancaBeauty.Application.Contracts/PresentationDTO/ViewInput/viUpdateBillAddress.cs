using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viUpdateBillAddress
    {
        [Display(Name = "BillId")]
        [RequiredString]
        [GUID]
        public string BillId { get; set; }

        [Display(Name = "AddressId")]
        [RequiredString]
        [GUID]
        public string AddressId { get; set; }
    }
}
