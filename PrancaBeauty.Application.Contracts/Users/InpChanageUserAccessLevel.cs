using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class InpChanageUserAccessLevel
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "SelfUserId")]
        [RequiredString]
        [GUID]
        public string SelfUserId { get; set; }

        [Display(Name = "AccessLevelId")]
        [RequiredString]
        [GUID]
        public string AccessLevelId { get; set; }
    }
}
