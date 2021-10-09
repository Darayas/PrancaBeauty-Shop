using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ModalFileUploader
    {
        public string FieldName { get; set; }
        public string UserId { get; set; }
        public IFormFile Files { get; set; }
    }
}
