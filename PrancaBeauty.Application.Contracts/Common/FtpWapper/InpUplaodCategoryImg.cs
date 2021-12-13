using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Common.FtpWapper
{
    public class InpUplaodCategoryImg
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "FormFile")]
        [FileSize(102400)]
        [AllowExtentions("image/jpg,image/jpeg")]
        public IFormFile FormFile { get; set; }

    }
}
