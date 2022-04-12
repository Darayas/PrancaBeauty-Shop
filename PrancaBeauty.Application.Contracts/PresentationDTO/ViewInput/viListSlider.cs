using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viListSlider
    {
        [Display(Name = "Title")]
        [MaxLengthString(150)]
        public string Title { get; set; }

        [Display(Name = "CurrentPage")]
        [NumRange(1, int.MaxValue)]
        public int CurrentPage { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue)]
        public int Take { get; set; } = 10;
    }
}
