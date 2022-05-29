using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_FilterByPrice
    {
        [Display(Name = "MinPrice")]
        [NumRange(0, 999999999)]
        public double MinPrice { get; set; }

        [Display(Name = "MaxPrice")]
        [NumRange(0, 999999999)]
        public double MaxPrice { get; set; }

        [Display(Name = "MinValue")]
        [NumRange(0, 999999999)]
        public double MinValue { get; set; }

        [Display(Name = "MaxValue")]
        [NumRange(0, 999999999)]
        public double MaxValue { get; set; }

        [Display(Name = "CurrencySymbol")]
        [RequiredString]
        [MaxLengthString(100)]
        public string CurrencySymbol { get; set; }


    }
}
