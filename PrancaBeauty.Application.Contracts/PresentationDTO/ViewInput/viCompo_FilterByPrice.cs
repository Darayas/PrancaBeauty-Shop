using Framework.Common.DataAnnotations.Numbers.All;
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

        
    }
}
