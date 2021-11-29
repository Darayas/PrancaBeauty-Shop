using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Address
{
    public class InpGetAddressByUserIdForManage
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Search")]
        [MaxLengthString(150, ErrorMessage = "MaxLengthMsg")]
        public string Search { get; set; }

        [Display(Name = "PageNum")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int PageNum { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Take { get; set; }
    }
}
