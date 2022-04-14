using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionProductAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.SectionProduct
{
    public class SectionProductApplication : ISectionProductApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISectionProductRepository _SectionProductRepository;

        public SectionProductApplication(ILogger logger, IServiceProvider serviceProvider, ISectionProductRepository sectionProductRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _SectionProductRepository=sectionProductRepository;
        }
    }
}
