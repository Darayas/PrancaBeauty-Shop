using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class InpRecoveryPasswordByEmailStep2
    {
        [Display(Name = "Token")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(1000, ErrorMessage = "MaxLength")]
        public string Token { get; set; }

        [Display(Name = "NewPassword")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string NewPassword { get; set; }
    }
}
