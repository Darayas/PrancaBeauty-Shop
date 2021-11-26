using Framework.Common.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class InpSaveAccountSettingUserDetails
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "DefaultLangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "UrlToChangeEmail")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(500, ErrorMessage = "MaxLength")]
        public string UrlToChangeEmail { get; set; }


        [Display(Name = "ProfileImage")]
        [FileSize(1572864, 102400, ErrorMessage = "FileSizeMsg")] // MaxSize: 1.5 MB , MinSize: 100 KB
        [AllowExtentions("image/jpg, image/png, image/jpeg", ErrorMessage = "AllowExtentionsMsg")]
        public IFormFile ProfileImage { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [EmailAddress(ErrorMessage = "EmailAddressMsg")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string LastName { get; set; }

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string BirthDate { get; set; }
    }
}
