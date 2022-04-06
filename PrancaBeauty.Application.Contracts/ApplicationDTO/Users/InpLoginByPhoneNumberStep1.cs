using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpLoginByPhoneNumberStep1
    {
        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [MaxLengthString(100)]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
