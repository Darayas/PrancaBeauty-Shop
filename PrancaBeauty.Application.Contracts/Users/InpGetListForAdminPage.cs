using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class InpGetListForAdminPage
    {
        [Display(Name = "Email")]
        [MaxLengthString(100)]
        [TemplEmail]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [MaxLengthString(100)]
        [PhoneNumber]
        public string PhoneNumber { get; set; }

        [Display(Name = "FullName")]
        [MaxLengthString(100)]
        public string FullName { get; set; }

        [Display(Name = "Sort")]
        [NumRange(1, int.MaxValue)]
        public string Sort { get; set; }

        [Display(Name = "PageNum")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int PageNum { get; set; }

        [Display(Name = "PageNum")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Take { get; set; }
    }
}
