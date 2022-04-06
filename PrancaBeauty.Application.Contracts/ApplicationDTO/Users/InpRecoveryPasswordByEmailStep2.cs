using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpRecoveryPasswordByEmailStep2
    {
        [Display(Name = "Token")]
        [RequiredString]
        [MaxLengthString(1000)]
        public string Token { get; set; }

        [Display(Name = "NewPassword")]
        [RequiredString]
        [MaxLengthString(100)]
        public string NewPassword { get; set; }
    }
}
