using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using Framework.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Bills
{
    public class InpGetListBillForManage
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "BuyerUserId")]
        [GUID]
        public string BuyerUserId { get; set; }

        [Display(Name = "SellerUserId")]
        [GUID]
        public string SellerUserId { get; set; }

        [Display(Name = "BillNumber")]
        [MaxLengthString(50)]
        public string BillNumber { get; set; }

        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue)]
        public int Take { get; set; } = 10;
    }
}
