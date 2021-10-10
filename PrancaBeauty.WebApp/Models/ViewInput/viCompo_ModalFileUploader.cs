using Microsoft.AspNetCore.Http;
using PrancaBeauty.WebApp.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ModalFileUploader
    {
        public string FieldName { get; set; }
        public string UserId { get; set; }

        [Display(Name = "File")]
        [RequiredFile(ErrorMessage = "RequiredFileMsg")]
        [FileSize(524288000, MinFileSize: 1, ErrorMessage = "FileSizeMsg")]
        [AllowExtentions(ErrorMessage = "AllowExtentionsMsg")]
        public IFormFile Files { get; set; }
    }
}
