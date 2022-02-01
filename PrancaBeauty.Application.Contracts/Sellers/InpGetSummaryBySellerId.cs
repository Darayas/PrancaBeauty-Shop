using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Sellers
{
    public class InpGetSummaryBySellerId
    {
        [Display(Name ="SellerId")]
        [RequiredString]
        [GUID]
        public string SellerId { get; set; }

        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }
    }
}
