using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_Login_PhoneNumberModel
    {
        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string PhoneNumber { get; set; }

        [Display(Name = "RemmeberMe")]
        public bool RemmeberMe { get; set; }
    }
}
