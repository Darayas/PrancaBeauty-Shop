using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Settings
{
    public class InpGetSetting
    {
        [Display(Name = "LangCode")]
        [RequiredString]
        [MaxLengthString(100)]
        public string LangCode { get; set; }
    }
}
