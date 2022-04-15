using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Common.FtpWapper
{
    public class InpUploadFromFileManager
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "FormFile")]
        [RequiredFile]
        [FileSize(10485760, MinFileSize: 10)]
        [AllowExtentions("image/jpg,image/png,image/gif,image/jpeg")]
        public List<IFormFile> FormFile { get; set; }
    }
}
