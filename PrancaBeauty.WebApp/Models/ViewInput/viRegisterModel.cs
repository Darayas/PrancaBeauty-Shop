using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viRegisterModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        [EmailAddress(ErrorMessage = "EmailAddressMsg")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "StringLengthMsg")]
        [RegularExpression(@"[A-Zا-یa-zآ\s]*")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        [RegularExpression(@"[A-Zا-یa-zآ\s]*")]
        public string LastName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "RequiredMsg")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [Required(ErrorMessage = "RequiredMsg")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "PassCompareMsg")]
        public string ConfirmPassword { get; set; }
    }
}
