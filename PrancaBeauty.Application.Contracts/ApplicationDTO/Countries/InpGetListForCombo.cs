using Framework.Common.DataAnnotations;
using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Countries
{
    public class InpGetListForCombo
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Search")]
        [MaxLengthString(100)]
        public string Search { get; set; }
    }
}
