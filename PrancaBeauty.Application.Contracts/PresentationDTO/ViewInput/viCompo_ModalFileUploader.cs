using Framework.Common.DataAnnotations;
using Framework.Common.DataAnnotations.File;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_ModalFileUploader
    {
        public string FieldName { get; set; }
        public string UserId { get; set; }

        [Display(Name = "File")]
        [RequiredFile]
        [FileSize(524288000, MinFileSize: 1)]
        [AllowExtentions("image/jpg,image/png,image/jpeg,application/zip,application/rar,video/mp4")]
        public IFormFile Files { get; set; }
    }
}
