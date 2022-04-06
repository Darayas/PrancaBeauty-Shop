using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_RecoveryByEmail
    {
        [Display(Name = "Email")]
        [RequiredString]
        [TemplEmail]
        public string Email { get; set; }
    }
}
