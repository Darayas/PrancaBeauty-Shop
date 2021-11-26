using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductVariants
{
    public class InpGetLstForCombo
    {
        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "LangId")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string Text { get; set; }
    }
}
