using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class InpGetListForCombo
    {

        [Display(Name = "LangId")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Name")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string Name { get; set; }
    }
}
