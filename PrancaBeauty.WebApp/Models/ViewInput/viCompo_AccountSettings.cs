using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

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
        [RequiredString]
        [MaxLengthString(100)]
        public string LangId { get; set; }

        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(100)]
        [TemplEmail]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [MaxLengthString(30)]
        [PhoneNumber(ErrorMessage = "PhoneNumberMsg")]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "FirstName")]
        [RequiredString]
        [MaxLengthString(100)]
        [RegularExpression(@"[A-Zا-یa-zآ\s]*")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [RequiredString]
        [MaxLengthString(100)]
        [RegularExpression(@"[A-Zا-یa-zآ\s]*")]
        public string LastName { get; set; }

        [Display(Name = "BirthDate")]
        [RequiredString]
        [MaxLengthString(100)]
        [Date(ErrorMessage = "DateMsg")]
        public string BirthDate { get; set; }

    }
}
