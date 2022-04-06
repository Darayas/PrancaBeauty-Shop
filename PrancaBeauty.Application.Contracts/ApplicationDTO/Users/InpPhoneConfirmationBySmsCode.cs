using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpPhoneConfirmationBySmsCode
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [MaxLengthString(100)]
        [PhoneNumber]
        public string PhoneNumber { get; set; }

        [Display(Name = "Code")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Code { get; set; }

    }
}
