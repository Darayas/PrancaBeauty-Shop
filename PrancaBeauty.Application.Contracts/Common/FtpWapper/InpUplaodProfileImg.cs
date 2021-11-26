using Framework.Common.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Common.FtpWapper
{
    public class InpUplaodProfileImg
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "FormFile")]
        [FileSize(102400, ErrorMessage = "FileSizeMsg")]
        [AllowExtentions("image/jpg,image/jpeg", ErrorMessage = "AllowExtentionsMsg")]
        public IFormFile FormFile { get; set; }
    }
}
