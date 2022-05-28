using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Keywords
{
    public class InpCheckIsKeywordAndHasProduct
    {
        [Display(Name = "KeywordTitle")]
        [RequiredString]
        [MaxLengthString(100)]
        public string KeywordTitle { get; set; }
    }
}
