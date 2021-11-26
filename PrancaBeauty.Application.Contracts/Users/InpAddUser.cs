using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class InpAddUser
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        [EmailAddress(ErrorMessage = "EmailAddressMsg")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "StringLengthMsg")]
        [PhoneNumber]
        public string PhoneNumber { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string LastName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "RequiredMsg")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [Required(ErrorMessage = "RequiredMsg")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "PassCompareMsg")]
        public string ConfirmPassword { get; set; }
    }
}
