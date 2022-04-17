using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viListShowcaseTabs
    {
        [Display(Name = "ShowcaseId")]
        [RequiredString]
        [GUID]
        public string ShowcaseId { get; set; }

        [Display(Name = "Title")]
        [MaxLengthString(100)]
        public string Title { get; set; }

    }
}
