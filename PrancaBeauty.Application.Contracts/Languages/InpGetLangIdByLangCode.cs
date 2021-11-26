using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Languages
{
    public class InpGetLangIdByLangCode
    {
        [Display(Name = "Code")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(50, ErrorMessage = "MaxLengthMsg")]
        public string Code { get; set; }
    }
}
