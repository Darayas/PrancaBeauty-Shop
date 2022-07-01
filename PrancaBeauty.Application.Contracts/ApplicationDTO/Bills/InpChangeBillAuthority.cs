using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Bills
{
    public class InpChangeBillAuthority
    {
        [Display(Name = "BillId")]
        [RequiredString]
        [GUID]
        public string BillId { get; set; }
        [Display(Name = "Authority")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Authority { get; set; }

        [Display(Name = "BuyerUserId")]
        [RequiredString]
        [GUID]
        public string BuyerUserId { get; set; }
    }
}
