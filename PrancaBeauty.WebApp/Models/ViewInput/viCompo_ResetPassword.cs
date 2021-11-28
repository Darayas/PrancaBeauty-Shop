using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ResetPassword
    {
        [RequiredString]
        public string Token { get; set; }

        [Display(Name = "NewPassword")]
        [RequiredString]
        public string NewPassword { get; set; }

        [Display(Name = "RetypeNewPassword")]
        [RequiredString]
        [Compare(nameof(NewPassword), ErrorMessage = "CompareMsg")]
        public string RetypeNewPassword { get; set; }
    }
}
