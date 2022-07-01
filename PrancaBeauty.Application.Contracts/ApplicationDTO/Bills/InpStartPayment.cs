using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Bills
{
    public class InpStartPayment
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "BillNumber")]
        [RequiredString]
        [MaxLengthString(100)]
        public string BillNumber { get; set; }
    }
}
