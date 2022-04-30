using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class InpSaveEditKeywordItem
    {
        [Display(Name = "SectionItemId")]
        [RequiredString]
        [GUID]
        public string SectionItemId { get; set; }

        [Display(Name = "KeywordId")]
        [RequiredString]
        [GUID]
        public string KeywordId { get; set; }

        public int CountFetch { get; set; }
        public InpSaveEditKeywordItemOrderByEnum OrderBy { get; set; }
    }

    public enum InpSaveEditKeywordItemOrderByEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
