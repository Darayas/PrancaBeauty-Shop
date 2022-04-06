using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpLoginByUserNamePassword
    {
        [Display(Name = "UserName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Password { get; set; }
    }
}
