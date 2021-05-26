using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viEditProfile
    {
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        public string LastName { get; set; }

        [Display(Name = "BirthDate")]
        public DateTime? BirthDate { get; set; }
    }
}
