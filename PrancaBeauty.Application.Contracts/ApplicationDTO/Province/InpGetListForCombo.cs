using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Province
{
    public class InpGetListForCombo
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "CountryId")]
        [RequiredString]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name = "Search")]
        [MaxLengthString(100, ErrorMessage = "MaxLengtMsg")]
        public string Search { get; set; }
    }
}
