using Framework.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class InpCheckHasUser
    {
        [Display(Name = "AccessLevelId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string AccessLevelId { get; set; }
    }
}
