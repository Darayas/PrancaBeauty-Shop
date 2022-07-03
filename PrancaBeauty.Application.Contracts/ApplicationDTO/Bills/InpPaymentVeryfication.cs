using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Bills
{
    public class InpPaymentVeryfication
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }
        
        [Display(Name = "CurrencyId")]
        [RequiredString]
        [GUID]
        public string CurrencyId { get; set; }

        [Display(Name = "BillNumber")]
        [RequiredString]
        [MaxLengthString(100)]
        public string BillNumber { get; set; }

        [Display(Name = "QueryData")]
        public Dictionary<string, string> QueryData { get; set; }
    }
}
