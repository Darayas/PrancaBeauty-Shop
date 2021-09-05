using PrancaBeauty.WebApp.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viAddProduct
    {
        public string ReturnUrl { get; set; }

        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID(ErrorMessage = "GUIDMsg")]
        public string LangId { get; set; }

        [Display(Name = "CategoryId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID(ErrorMessage = "GUIDMsg")]
        public string CategoryId { get; set; }

    }
}
