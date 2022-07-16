using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_BillAddress
    {
        [Display(Name = "BillId")]
        [RequiredString]
        [GUID]
        public string BillId { get; set; }

        [Display(Name = "AddressId")]
        [GUID]
        public string AddressId { get; set; }

        [Display(Name = "BuyerUserId")]
        [RequiredString]
        [GUID]
        public string BuyerUserId { get; set; }

        public bool IsBuyer { get; set; }
        public bool IsPayed { get; set; }
    }
}
