using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viBill
    {
        [Display(Name = "BillNumber")]
        [RequiredString]
        [MaxLengthString(13)]
        public string BillNumber { get; set; }
    }
}
