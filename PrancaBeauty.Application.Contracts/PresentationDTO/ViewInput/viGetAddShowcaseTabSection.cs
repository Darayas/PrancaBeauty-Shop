using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viGetAddShowcaseTabSection
    {
        [Display(Name = "ShowcaseId")]
        [RequiredString]
        [GUID]
        public string ShowcaseId { get; set; }

        [Display(Name = "ShowcaseTabId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabId { get; set; }
    }
}
