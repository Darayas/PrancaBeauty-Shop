using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpChanagePassword
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "CurrentPassword")]
        [RequiredString]
        [MaxLengthString(100)]
        public string CurrentPassword { get; set; }

        [Display(Name = "NewPassword")]
        [RequiredString]
        [MaxLengthString(100)]
        public string NewPassword { get; set; }
    }
}
