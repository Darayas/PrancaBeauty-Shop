using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class InpAddTabSectionCategoryItem
    {
        [Display(Name = "ShowcaseTabSectionId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabSectionId { get; set; }

        [Display(Name = "CategoryId")]
        [RequiredString]
        [GUID]
        public string CategoryId { get; set; }

        [Display(Name = "CountFetch")]
        [RequiredString]
        [NumRange(1, 30)]
        public int CountFetch { get; set; }

        [Display(Name = "OrderBy")]
        public InpAddTabSectionCategoryItemEnum OrderBy { get; set; }
    }

    public enum InpAddTabSectionCategoryItemEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
