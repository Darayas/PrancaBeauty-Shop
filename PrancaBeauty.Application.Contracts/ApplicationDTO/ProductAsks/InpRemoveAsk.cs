using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductAsks
{
    public class InpRemoveAsk
    {
        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "AskId")]
        [RequiredString]
        [GUID]
        public string AskId { get; set; }
    }
}
