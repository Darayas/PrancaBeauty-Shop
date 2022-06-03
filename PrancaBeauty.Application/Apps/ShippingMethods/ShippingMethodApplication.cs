using Framework.Infrastructure;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShippingMethods
{
    public class ShippingMethodApplication: IShippingMethodApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShippingMethodRepository _ShippingMethodRepository;
        public ShippingMethodApplication(ILogger logger, IServiceProvider serviceProvider, IShippingMethodRepository shippingMethodRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShippingMethodRepository=shippingMethodRepository;
        }
    }
}
