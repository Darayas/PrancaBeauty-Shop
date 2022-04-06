using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpGetUserByPhoneNumber
    {
        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [MaxLengthString(50)]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
