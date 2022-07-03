using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.BillItems
{
    public class InpBillItemFinalPriceRegistration
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "TaxPercent")]
        public double TaxPercent { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }

        [Display(Name = "TotalPrice")]
        public double TotalPrice { get; set; }
    }
}
