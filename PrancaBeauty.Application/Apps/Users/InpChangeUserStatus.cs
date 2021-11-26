using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class InpChangeUserStatus
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "SelfUserId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string SelfUserId { get; set; }
    }
}
