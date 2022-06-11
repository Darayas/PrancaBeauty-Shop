using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Bills
{
    public class InpGetBillDetails
    {
        [Display(Name = "BillNumber")]
        [RequiredString]
        [MaxLengthString(13)]
        public string BillNumber { get; set; }
    }
}
