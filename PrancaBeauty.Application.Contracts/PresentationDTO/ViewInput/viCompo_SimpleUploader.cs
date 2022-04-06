using Framework.Common.DataAnnotations.File;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_SimpleUploader
    {
        [Display(Name = "File")]
        [RequiredFile]
        [FileSize(10485760, MinFileSize: 10)]
        [AllowExtentions("image/jpg,image/png,image/gif,image/jpeg")]
        public IFormFile upload { get; set; }
    }
}
