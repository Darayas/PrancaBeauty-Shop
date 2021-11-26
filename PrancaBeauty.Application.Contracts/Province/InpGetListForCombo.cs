using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Province
{
    public class InpGetListForCombo
    {
        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "CountryId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name = "Search")]
        [MaxLength(100,ErrorMessage = "MaxLengtMsg")]
        public string Search { get; set; }
    }
}
