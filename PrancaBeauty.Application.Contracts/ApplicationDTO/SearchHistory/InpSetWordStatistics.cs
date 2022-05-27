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

        [Display(Name = "Keyword")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Keyword { get; set; }
    }
}
