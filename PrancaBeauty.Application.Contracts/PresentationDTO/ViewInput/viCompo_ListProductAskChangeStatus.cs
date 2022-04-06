using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_ListProductAskChangeStatus
    {
        [Display(Name = "AskId")]
        [RequiredString]
        [GUID]
        public string AskId { get; set; }
    }
}
