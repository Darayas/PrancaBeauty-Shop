using Framework.Infrastructure;
using PrancaBeauty.Domin.PaymentGate.PaymentGateRestrictAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.PaymentGateRestricts
{
    public class PaymentGateRestrictApplication : IPaymentGateRestrictApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IPaymentGateRestrictRepository _PaymentGateRestrictRepository;

        public PaymentGateRestrictApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IPaymentGateRestrictRepository paymentGateRestrictRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _PaymentGateRestrictRepository=paymentGateRestrictRepository;
        }
    }
}
