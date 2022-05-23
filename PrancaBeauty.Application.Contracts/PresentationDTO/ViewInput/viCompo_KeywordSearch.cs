using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_KeywordSearch
    {
        [Display(Name = "KeywordTitle")]
        [MaxLengthString(100)]
        public string KeywordTitle { get; set; }
    }
}
