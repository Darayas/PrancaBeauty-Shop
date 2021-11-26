using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class InpLogin
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string Password { get; set; }
    }
}
