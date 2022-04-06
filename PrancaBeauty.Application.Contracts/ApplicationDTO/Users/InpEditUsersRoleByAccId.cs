using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpEditUsersRoleByAccId
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string AccessLevelId { get; set; }

        public List<InpEditUsersRoleByAccId_Roles> Roles { get; set; }
    }

    public class InpEditUsersRoleByAccId_Roles
    {
        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }
    }
}
