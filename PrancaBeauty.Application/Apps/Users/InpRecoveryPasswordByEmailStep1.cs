using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class InpRecoveryPasswordByEmailStep1
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "ResetLinkTemplate")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(500, ErrorMessage = "MaxLength")]
        public string ResetLinkTemplate { get; set; }
    }
}
