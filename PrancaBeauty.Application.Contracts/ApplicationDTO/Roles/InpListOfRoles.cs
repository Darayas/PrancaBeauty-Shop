using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Roles
{
    public class InpListOfRoles
    {
        [Display(Name = "ParentId")]
        [GUID]
        public string ParentId { get; set; }
    }
}
