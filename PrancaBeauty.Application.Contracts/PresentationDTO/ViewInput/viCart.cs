using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCart
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "Qty")]
        [RequiredString]
        [NumRange(1, 100)]
        public int Qty { get; set; }
    }
}
