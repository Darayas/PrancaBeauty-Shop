using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ChanageAccessLevel
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [MaxLengthString(100)]
        public string UserId { get; set; }

        [Display(Name = "AccessLevelId")]
        [RequiredString]
        [MaxLengthString(100)]
        public string AccessLevelId { get; set; }
    }
}
