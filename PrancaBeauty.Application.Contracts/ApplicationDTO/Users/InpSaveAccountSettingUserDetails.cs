using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpSaveAccountSettingUserDetails
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "DefaultLangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "UrlToChangeEmail")]
        [RequiredString]
        [MaxLengthString(500)]
        public string UrlToChangeEmail { get; set; }


        [Display(Name = "ProfileImage")]
        [FileSize(1572864, 102400)] // MaxSize: 1.5 MB , MinSize: 100 KB
        [AllowExtentions("image/jpg, image/png, image/jpeg")]
        public IFormFile ProfileImage { get; set; }


        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(100)]
        [TemplEmail]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [MaxLengthString(100)]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "FirstName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string LastName { get; set; }

        [Display(Name = "BirthDate")]
        [RequiredString]
        [MaxLengthString(100)]
        public string BirthDate { get; set; }
    }
}
