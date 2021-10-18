using Microsoft.AspNetCore.Http;
using PrancaBeauty.WebApp.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_SimpleUploader
    {
        [Display(Name ="File")]
        [RequiredFile(ErrorMessage = "RequiredFileMsg")]
        [FileSize(10485760,MinFileSize:10,ErrorMessage = "FileSizeMsg")]
        [AllowExtentions("image/jpg,image/png,image/gif,image/jpeg",ErrorMessage = "AllowExtentionsMsg")]
        public IFormFile upload{ get; set; }
    }
}
