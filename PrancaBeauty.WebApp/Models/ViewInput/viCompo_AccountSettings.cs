using PrancaBeauty.WebApp.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_AccountSettings
    {
        [Display(Name = "DefaultLangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string LangId { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        [EmailAddress(ErrorMessage = "EmailAddressMsg")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "StringLengthMsg")]
        [PhoneNumber(ErrorMessage = "PhoneNumberMsg")]
        public string PhoneNumber { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        [RegularExpression(@"[A-Zا-یa-zآ\s]*")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        [RegularExpression(@"[A-Zا-یa-zآ\s]*")]
        public string LastName { get; set; }

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        [Date(ErrorMessage = "DateMsg")]
        public string BirthDate { get; set; }

    }
}
