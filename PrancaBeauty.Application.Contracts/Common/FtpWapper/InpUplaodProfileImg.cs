using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Common.FtpWapper
{
    public class InpUplaodProfileImg
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "FormFile")]
        [FileSize(1572864, 102400)] // MaxSize: 1.5 MB , MinSize: 100 KB
        [AllowExtentions("image/jpg, image/png, image/jpeg")]
        public IFormFile FormFile { get; set; }
    }
}
