using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class InpEditUsersRoleByAccId
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string AccessLevelId { get; set; }

        public List<InpEditUsersRoleByAccId_Roles> Roles { get; set; }
    }

    public class InpEditUsersRoleByAccId_Roles
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string Name { get; set; }
    }
}
