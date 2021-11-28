using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Templates
{
    public class InpGetEmailChangeTemplate
    {
        [Display(Name = "LangCode")]
        [RequiredString]
        [MaxLengthString(100)]
        public string LangCode { get; set; }

        [Display(Name = "Url")]
        [RequiredString]
        [MaxLengthString(500)]
        public string Url { get; set; }
    }
}
