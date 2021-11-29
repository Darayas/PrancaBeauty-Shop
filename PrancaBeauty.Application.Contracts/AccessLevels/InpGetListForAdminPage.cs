using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class InpGetListForAdminPage
    {
        [Display(Name = "Title")]
        [MaxLengthString(150)]
        public string Title { get; set; }

        [Display(Name = "PageNum")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsg")]
        public int PageNum { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsg")]
        public int Take { get; set; }
    }
}
