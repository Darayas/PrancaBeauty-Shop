using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders
{
    public class InpSortingSlider
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "Action")]
        [RequiredString]
        public InpSortingSliderItem Action { get; set; }
    }

    public enum InpSortingSliderItem
    {
        Up,
        Down
    }
}
