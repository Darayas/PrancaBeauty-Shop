using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductAskLikes
{
    public class InpDisLikeAsk
    {
        [Display(Name = "AskId")]
        [RequiredString]
        [GUID]
        public string AskId { get; set; }

        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }
    }
}
