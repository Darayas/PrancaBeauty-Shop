using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Common.FtpWapper
{
    public class InpUplaodCategoryImg
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "FormFile")]
        [FileSize(102400)]
        [AllowExtentions("image/jpg,image/jpeg,image/png")]
        public IFormFile FormFile { get; set; }

    }
}
