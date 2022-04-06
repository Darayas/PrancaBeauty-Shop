using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpLoginByEmailLinkStep1
    {
        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(100)]
        [TemplEmail]
        public string Email { get; set; }

        [Display(Name = "IP")]
        [MaxLengthString(50)]
        public string IP { get; set; }
    }
}
