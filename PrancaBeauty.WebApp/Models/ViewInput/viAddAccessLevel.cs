using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viAddAccessLevel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        public string Name { get; set; }

        [Display(Name = "Roles")]
        public string[] Roles { get; set; }
    }
}
