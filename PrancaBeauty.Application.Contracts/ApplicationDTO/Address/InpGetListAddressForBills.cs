using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Address
{
    public class InpGetListAddressForBills
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "AddressId")]
        [GUID]
        public string AddressId { get; set; }
        public bool IsBuyer { get; set; }

        public bool IsPayed { get; set; }
    }
}
