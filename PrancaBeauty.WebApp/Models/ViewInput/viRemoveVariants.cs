using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viRemoveVariants
    {
        [Display(Name ="Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "Index")]
        [RequiredString]
        public string Index { get; set; }
    }
}
