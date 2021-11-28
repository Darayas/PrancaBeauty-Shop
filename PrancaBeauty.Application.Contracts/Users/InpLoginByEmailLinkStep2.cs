using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class InpLoginByEmailLinkStep2
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Password { get; set; }

        [Display(Name = "LinkIP")]
        [RequiredString]
        [MaxLengthString(100)]
        public string LinkIP { get; set; }

        [Display(Name = "UserIP")]
        [RequiredString]
        [MaxLengthString(100)]
        public string UserIP { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }
    }
}
