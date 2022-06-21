using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Address
{
    public class InpGetAddressById
    {
        [Display(Name = "AddressId")]
        [RequiredString]
        [GUID]
        public string AddressId { get; set; }
    }
}
