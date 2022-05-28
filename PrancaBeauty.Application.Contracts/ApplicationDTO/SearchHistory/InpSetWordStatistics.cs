using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.SearchHistory
{
    public class InpSetWordStatistics
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "KeywordTitle")]
        [RequiredString]
        [MaxLengthString(100)]
        public string KeywordTitle { get; set; }
    }
}
