using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viMainPageShowcase
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "CountryId")]
        [RequiredString]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name = "CurrencyId")]
        [RequiredString]
        [GUID]
        public string CurrencyId { get; set; }
    }
}
