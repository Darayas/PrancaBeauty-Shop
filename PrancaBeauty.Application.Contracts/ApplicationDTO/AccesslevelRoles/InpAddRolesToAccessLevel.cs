using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.AccesslevelRoles
{
    public class InpAddRolesToAccessLevel
    {
        [Display(Name = "AccessLevelId")]
        [RequiredString]
        [GUID]
        public string AccessLevelId { get; set; }

        [Display(Name = "RolesName")]
        public string[] RolesName { get; set; }
    }
}
