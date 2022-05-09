using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Languages
{
    public class InpGetCountryIdByLangCode
    {
        [Display(Name = "Code")]
        [RequiredString]
        [MaxLengthString(50)]
        public string Code { get; set; }
    }
}
