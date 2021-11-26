using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class InpLoginByPhoneNumberStep1
    {
        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
