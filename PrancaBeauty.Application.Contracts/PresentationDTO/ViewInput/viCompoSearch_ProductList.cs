using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompoSearch_ProductList
    {
        [Display(Name = "CategoryName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string CategoryName { get; set; }

        [Display(Name = "KeywordName")]
        [MaxLengthString(100)]
        public string KeywordName { get; set; }

        [Display(Name = "Take")]
        [NumRange(1,int.MaxValue)]
        public int Take { get; set; }

        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue)]
        public int CurrentPage { get; set; }
    }
}
