using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class InpGetListForAdminPage
    {
        [Display(Name = "Email")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [EmailAddress(ErrorMessage = "EmailAddressMsg")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [PhoneNumber]
        public string PhoneNumber { get; set; }

        [Display(Name = "FullName")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string FullName { get; set; }

        [Display(Name = "Sort")]
        [Range(1,int.MaxValue,ErrorMessage = "RangeMsg")]
        public string Sort { get; set; }

        [Display(Name = "PageNum")]
        [Range(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int PageNum { get; set; }

        [Display(Name = "PageNum")]
        [Range(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Take { get; set; }
    }
}
