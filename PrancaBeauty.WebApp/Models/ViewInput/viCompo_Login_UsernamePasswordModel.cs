using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_Login_UsernamePasswordModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string Password { get; set; }

        [Display(Name = "RemmeberMe")]
        public bool RemmeberMe { get; set; }
    }
}
