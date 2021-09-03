using Microsoft.AspNetCore.Http;
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
        [Display(Name = "ProfileImage")]
        //[RequiredFile(ErrorMessage = "RequiredFileMsg")]
        [FileSize(1572864, 102400, ErrorMessage = "FileSizeMsg")] // MaxSize: 1.5 MB , MinSize: 100 KB
        [AllowExtentions("image/jpg, image/png, image/jpeg", ErrorMessage = "AllowExtentionsMsg")]
        public IFormFile ProfileImage { get; set; }

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
        public bool PhoneNumberConfirmed { get; set; }

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
