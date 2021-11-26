using Framework.Common.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Common.FtpWapper
{
    public class InpUploadFromFileManager
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
