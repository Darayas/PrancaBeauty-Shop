using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.City
{
    public class InpGetListForCombo
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "ProvinceId")]
        [RequiredString]
        [GUID]
        public string ProvinceId { get; set; }

        [Display(Name = "Search")]
        [MaxLengthString(100)]
        public string Search { get; set; }
    }
}
