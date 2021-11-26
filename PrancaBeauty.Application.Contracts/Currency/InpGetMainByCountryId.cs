using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Currency
{
    public class InpGetMainByCountryId
    {
        [Display(Name = "LangId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "CountryId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string CountryId { get; set; }
    }
}
