using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viAddSectionKeywordItem
    {
        [Display(Name = "ShowcaseTabSectionId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabSectionId { get; set; }

        [Display(Name = "KeywordId")]
        [RequiredString]
        [GUID]
        public string KeywordId { get; set; }

        [Display(Name = "CountFetch")]
        [RequiredString]
        [NumRange(1, 30)]
        public int CountFetch { get; set; }

        [Display(Name = "OrderBy")]
        public viAddSectionKeywordItemEnum OrderBy { get; set; }
    }

    public enum viAddSectionKeywordItemEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
