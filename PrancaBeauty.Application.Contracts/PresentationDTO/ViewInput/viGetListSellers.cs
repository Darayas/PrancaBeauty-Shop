using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viGetListSellers
    {
        [Display]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
    }
}
