using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_HowToPay
    {
        [Display(Name = "BillId")]
        [RequiredString]
        [GUID]
        public string BillId { get; set; }

        [Display(Name = "GateId")]
        [GUID]
        public string GateId { get; set; }

        public bool IsBuyer { get; set; }

        public bool IsPayed { get; set; }

    }
}
