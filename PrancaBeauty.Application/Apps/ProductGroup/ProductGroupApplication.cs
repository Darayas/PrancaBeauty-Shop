using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.ProductGroup
{
    public class ProductGroupApplication : IProductGroupApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductGroupRepository _ProductGroupRepository;

        public ProductGroupApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IProductGroupRepository productGroupRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ProductGroupRepository=productGroupRepository;
        }
    }
}
