using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ResetPassword
    {
        [Required]
        public string Token { get; set; }

        [Display(Name = "NewPassword")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string NewPassword { get; set; }

        [Display(Name = "RetypeNewPassword")]
        [Required(ErrorMessage = "RequiredMsg")]
        [Compare(nameof(NewPassword),ErrorMessage = "CompareMsg")]
        public string RetypeNewPassword { get; set; }
    }
}
