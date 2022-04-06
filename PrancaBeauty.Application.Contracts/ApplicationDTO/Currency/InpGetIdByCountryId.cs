using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Currency
{
    public class InpGetIdByCountryId
    {
        [Display(Name = "CountryId")]
        [RequiredString]
        [GUID]
        public string CountryId { get; set; }
    }
}
