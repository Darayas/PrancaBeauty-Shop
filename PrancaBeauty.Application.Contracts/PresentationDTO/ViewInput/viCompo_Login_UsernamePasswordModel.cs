using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_Login_UsernamePasswordModel
    {
        [Display(Name = "Email")]
        [RequiredString]
        [TemplEmail]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        public string Password { get; set; }

        [Display(Name = "RemmeberMe")]
        public bool RemmeberMe { get; set; }
    }
}
