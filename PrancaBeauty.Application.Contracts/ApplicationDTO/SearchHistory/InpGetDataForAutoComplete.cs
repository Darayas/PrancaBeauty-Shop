using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.SearchHistory
{
    public class InpGetDataForAutoComplete
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "KeywordTitle")]
        [MaxLengthString(100)]
        public string KeywordTitle { get; set; }


    }
}
