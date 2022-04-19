using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viListShowcaseTabSection
    {
        [Display(Name = "ShowcaseTabId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabId { get; set; }


        [Display(Name = "Name")]
        [MaxLengthString(100)]
        public string Name { get; set; }

        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue)]
        public int Page { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue)]
        public int Take { get; set; }
    }
}
