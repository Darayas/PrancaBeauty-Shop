using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ChangePassword
    {
        [Display(Name = "CurrentPassword")]
        [Required(ErrorMessage = "RequiredMsg")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "NewPassword")]
        [Required(ErrorMessage = "RequiredMsg")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "ConfirmPassword")]
        [Required(ErrorMessage = "RequiredMsg")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "CompareMsg")]
        public string ConfirmPassword { get; set; }

    }
}
