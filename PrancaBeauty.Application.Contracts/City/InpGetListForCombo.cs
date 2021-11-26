using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.City
{
    public class InpGetListForCombo
    {
        [Display(Name = "LangId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "ProvinceId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string ProvinceId { get; set; }

        [Display(Name = "Search")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string Search { get; set; }
    }
}
