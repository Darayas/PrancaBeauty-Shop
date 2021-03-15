using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_Login_PhoneNumber_OTPModel
    {
        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "StringLengthMsg")]
        public string Code { get; set; }

        [Display(Name = "RemmeberMe")]
        public bool RemmeberMe { get; set; }
    }
}
