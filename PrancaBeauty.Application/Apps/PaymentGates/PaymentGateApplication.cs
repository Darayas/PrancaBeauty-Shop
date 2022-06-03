using AutoMapper;
using Framework.Infrastructure;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.PaymentGates
{
    public class PaymentGateApplication : IPaymentGateApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IMapper _Mapper;
        private readonly IPaymentGateRepository _PaymentGateRepository;
        public PaymentGateApplication(ILogger logger, IServiceProvider serviceProvider, IMapper mapper, IPaymentGateRepository paymentGateRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _Mapper=mapper;
            _PaymentGateRepository=paymentGateRepository;
        }
    }
}
