using Framework.Infrastructure;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.ShippingMethodRestricts
{
    public class ShippingMethodRestrictApplication : IShippingMethodRestrictApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShippingMethodRepository _ShippingMethodRepository;

        public ShippingMethodRestrictApplication(ILogger logger, IServiceProvider serviceProvider, IShippingMethodRepository shippingMethodRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShippingMethodRepository=shippingMethodRepository;
        }
    }
}
