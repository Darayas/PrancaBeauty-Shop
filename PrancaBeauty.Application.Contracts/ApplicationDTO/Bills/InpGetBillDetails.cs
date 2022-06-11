using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Bills
{
    public class InpGetBillDetails
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "CurrencyId")]
        [RequiredString]
        [GUID]
        public string CurrencyId { get; set; }

        [Display(Name = "BillNumber")]
        [RequiredString]
        [MaxLengthString(13)]
        public string BillNumber { get; set; }
    }
}
