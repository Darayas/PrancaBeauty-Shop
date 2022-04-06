using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Categories
{
    public class InpGetListForAdminPage
    {
        [Display(Name = "PageNum")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Title")]
        [MaxLengthString(150)]
        public string Title { get; set; }

        [Display(Name = "ParentTitle")]
        [MaxLengthString(150)]
        public string ParentTitle { get; set; }

        [Display(Name = "PageNum")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int PageNum { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Take { get; set; }
    }
}
