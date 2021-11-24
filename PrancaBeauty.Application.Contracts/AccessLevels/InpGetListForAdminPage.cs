using Framework.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class InpGetListForAdminPage
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(150, ErrorMessage = "MaxLengthMsg")]
        public string Title { get; set; }

        [Display(Name = "PageNum")]
        [Range(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsg")]
        public int PageNum { get; set; }

        [Display(Name = "Take")]
        [Range(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsg")]
        public int Take { get; set; }
    }
}
