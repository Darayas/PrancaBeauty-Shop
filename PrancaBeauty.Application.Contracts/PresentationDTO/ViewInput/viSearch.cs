using Framework.Application.Enums;
using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viSearch
    {
        [Display(Name = "CategoryName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string CategoryName { get; set; }

        [Display(Name = "KeywordTitle")]
        [MaxLengthString(100)]
        public string KeywordTitle { get; set; }

        [Display(Name = "MinPrice")]
        public double MinPrice { get; set; }

        [Display(Name = "MaxPrice")]
        public double MaxPrice { get; set; }

        [Display(Name = "Sort")]
        public GetProductListForAdvanceSearchSortingEnum Sort { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue)]
        public int Take { get; set; } = 10;

        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue)]
        public int CurrentPage { get; set; } = 1;

        [Display(Name = "OnlyExistProducts")]
        public bool OnlyExistProducts { get; set; }

        [Display(Name = "OnlySendByPrancaBeauty")]
        public bool OnlySendByPrancaBeauty { get; set; }

        [Display(Name = "OnlySendBySeller")]
        public bool OnlySendBySeller { get; set; }
    }
}
