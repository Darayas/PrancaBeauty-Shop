using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_ListProductAskDisLike
    {
        [Display(Name = "AskId")]
        [RequiredString]
        [GUID]
        public string AskId { get; set; }
    }
}
