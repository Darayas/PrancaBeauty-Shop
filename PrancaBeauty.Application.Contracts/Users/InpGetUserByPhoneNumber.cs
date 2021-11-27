using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class InpGetUserByPhoneNumber
    {
        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(50, ErrorMessage = "MaxLength")]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
