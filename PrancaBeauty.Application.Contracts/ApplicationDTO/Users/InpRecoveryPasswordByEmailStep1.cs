using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpRecoveryPasswordByEmailStep1
    {
        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(100)]
        [TemplEmail]
        public string Email { get; set; }

        [Display(Name = "ResetLinkTemplate")]
        [RequiredString]
        [MaxLengthString(500)]
        public string ResetLinkTemplate { get; set; }
    }
}
