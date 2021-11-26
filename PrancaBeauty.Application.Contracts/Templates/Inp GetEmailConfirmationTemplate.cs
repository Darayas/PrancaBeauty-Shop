using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Templates
{
    public class InpGetEmailConfirmationTemplate
    {
        [Display(Name = "LangCode")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string LangCode { get; set; }

        [Display(Name = "Url")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(500, ErrorMessage = "MaxLengthMsg")]
        public string Url { get; set; }
    }
}
