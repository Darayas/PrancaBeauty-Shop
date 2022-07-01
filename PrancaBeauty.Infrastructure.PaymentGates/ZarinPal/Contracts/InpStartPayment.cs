using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Infrastructure.PaymentGates.ZarinPal.Contracts
{
    public class InpStartPayment
    {
        [Display(Name = "Amount")]
        public double Amount { get; set; }

        [Display(Name = "Description")]
        [MaxLengthString(250)]
        public string Description { get; set; }

        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        [RequiredString]
        [MaxLengthString(20)]
        public string Mobile { get; set; }

        [Display(Name = "CallBackUrl")]
        [RequiredString]
        [MaxLengthString(200)]
        public string CallBackUrl { get; set; }
    }
}
