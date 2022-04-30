using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class InpSaveEditCategoryItem
    {
        [Display(Name = "SectionItemId")]
        [RequiredString]
        [GUID]
        public string SectionItemId { get; set; }

        [Display(Name = "CategoryId")]
        [RequiredString]
        [GUID]
        public string CategoryId { get; set; }

        public int CountFetch { get; set; }
        public InpSaveEditCategoryItemOrderByEnum OrderBy { get; set; }
    }

    public enum InpSaveEditCategoryItemOrderByEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
