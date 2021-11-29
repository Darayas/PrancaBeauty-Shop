using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Common.FtpWapper
{
    public class InpUploadFromFileManager
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "FormFile")]
        [RequiredFile(ErrorMessage = "RequiredFileMsg")]
        [FileSize(10485760, MinFileSize: 10, ErrorMessage = "FileSizeMsg")]
        [AllowExtentions("image/jpg,image/png,image/gif,image/jpeg", ErrorMessage = "AllowExtentionsMsg")]
        public IFormFile FormFile { get; set; }
    }
}
