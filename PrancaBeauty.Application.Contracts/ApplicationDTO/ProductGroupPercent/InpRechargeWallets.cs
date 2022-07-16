using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductGroupPercent
{
    public class InpRechargeWallets
    {
        [Display(Name = "BillId")]
        [RequiredString]
        [GUID]
        public string BillId { get; set; }
        
        [Display(Name = "CurrencyId")]
        [RequiredString]
        [GUID]
        public string CurrencyId { get; set; }
    }
}
