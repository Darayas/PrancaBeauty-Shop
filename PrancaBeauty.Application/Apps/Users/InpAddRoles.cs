using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class InpAddRoles
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string UserId { get; set; }

        public List<InpAddRolesItem> Roles { get; set; }
    }

    public class InpAddRolesItem
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string Name { get; set; }
    }
}
