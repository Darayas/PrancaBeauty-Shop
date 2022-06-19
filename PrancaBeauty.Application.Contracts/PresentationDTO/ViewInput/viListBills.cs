using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viListBills
    {        
        [Display(Name= "BuyerUserId")]
        [GUID]
        public string BuyerUserId { get; set; }
        
        [Display(Name= "SellerUserId")]
        [GUID]
        public string SellerUserId { get; set; }

        [Display(Name = "BillNumber")]
        [MaxLengthString(50)]
        public string BillNumber { get; set; }

    }
}
