using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class InpGetUserIdsByAccId
    {
        [Display(Name = "AccessLevelId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string AccessLevelId { get; set; }
    }
}
