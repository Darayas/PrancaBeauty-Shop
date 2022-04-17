using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viGetListShowcaseTabs
    {
        [Display(Name = "ShowcaseId")]
        [RequiredString]
        [GUID]
        public string ShowcaseId { get; set; }
    }
}
