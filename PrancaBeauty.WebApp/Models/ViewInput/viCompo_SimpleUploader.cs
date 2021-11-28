using Framework.Common.DataAnnotations.File;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_SimpleUploader
    {
        [Display(Name = "File")]
        [RequiredFile(ErrorMessage = "RequiredFileMsg")]
        [FileSize(10485760, MinFileSize: 10, ErrorMessage = "FileSizeMsg")]
        [AllowExtentions("image/jpg,image/png,image/gif,image/jpeg", ErrorMessage = "AllowExtentionsMsg")]
        public IFormFile upload { get; set; }
    }
}
