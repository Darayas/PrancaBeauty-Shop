using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_BillAddress
    {
        [Display(Name = "AddressId")]
        [GUID]
        public string AddressId { get; set; }

        [Display(Name = "BuyerUserId")]
        [RequiredString]
        [GUID]
        public string BuyerUserId { get; set; }
    }
}
