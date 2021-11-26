using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Languages
{
    public class InpGetCountryIdByLangId
    {
        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }
    }
}
