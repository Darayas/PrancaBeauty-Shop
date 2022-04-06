using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.AccesslevelRoles
{
    public class InpRemoveByAccessLevelId
    {
        [Display(Name = "AccessLevelId")]
        [RequiredString]
        [GUID]
        public string AccessLevelId { get; set; }
    }
}
