using Framework.Common.DataAnnotations;
using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariants
{
    public class InpGetLstForCombo
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "LangId")]
        [MaxLengthString(100)]
        public string Text { get; set; }
    }
}
