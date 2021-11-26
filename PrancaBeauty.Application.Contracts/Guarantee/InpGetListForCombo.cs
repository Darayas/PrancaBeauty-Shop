using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Guarantee
{
    public class InpGetListForCombo
    {
        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "LangId")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string Search { get; set; }
    }
}
