using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class InpAddTabSectionKeywordItem
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
        public InpAddTabSectionKeywordItemEnum OrderBy { get; set; }
    }

    public enum InpAddTabSectionKeywordItemEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
