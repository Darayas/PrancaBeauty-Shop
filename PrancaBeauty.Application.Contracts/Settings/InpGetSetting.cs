using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Settings
{
    public class InpGetSetting
    {
        [Display(Name = "LangCode")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string LangCode { get; set; }
    }
}
