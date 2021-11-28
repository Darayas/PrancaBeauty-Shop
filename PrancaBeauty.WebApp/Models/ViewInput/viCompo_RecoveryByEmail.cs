using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_RecoveryByEmail
    {
        [Display(Name = "Email")]
        [RequiredString]
        [TemplEmail]
        public string Email { get; set; }
    }
}
