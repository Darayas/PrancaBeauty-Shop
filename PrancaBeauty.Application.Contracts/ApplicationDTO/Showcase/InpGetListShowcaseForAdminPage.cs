using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase
{
    public class InpGetListShowcaseForAdminPage
    {
        [Display(Name ="LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "LangId")]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name ="Title")]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "Page")]
        [NumRange(1,int.MaxValue)]
        public int Page { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue)]
        public int Take { get; set; }
    }
}
