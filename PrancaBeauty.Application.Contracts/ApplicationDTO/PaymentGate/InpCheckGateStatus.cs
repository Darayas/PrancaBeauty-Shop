using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.PaymentGate
{
    public class InpCheckGateStatus
    {
        [Display(Name = "GateName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string GateName { get; set; }
    }
}
