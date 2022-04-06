using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_Login_PhoneNumber_OTPModel
    {
        [Display(Name = "PhoneNumber")]
        [RequiredString]
        public string PhoneNumber { get; set; }

        [Display(Name = "Code")]
        [RequiredString]
        [MaxLengthString(8)]
        public string Code { get; set; }

        [Display(Name = "RemmeberMe")]
        public bool RemmeberMe { get; set; }
    }
}
