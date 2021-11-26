using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Languages
{
    public class InpIsValidAbbrForSiteLang
    {
        [Display(Name = "Abbr")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(50, ErrorMessage = "MaxLengthMsg")]
        public string Abbr { get; set; }
    }
}
