using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpChangeEmail
    {
        [Display(Name = "Token")]
        [RequiredString]
        [MaxLengthString(1000)]
        public string Token { get; set; }
    }
}
