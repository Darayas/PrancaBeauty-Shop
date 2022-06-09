using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductGroupPercentAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.ProductGroupPercent
{
    public class ProductGroupPercentApplication : IProductGroupPercentApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductGroupPercentRepository _ProductGroupPercentRepository;

        public ProductGroupPercentApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IProductGroupPercentRepository productGroupPercentRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ProductGroupPercentRepository=productGroupPercentRepository;
        }
    }
}
