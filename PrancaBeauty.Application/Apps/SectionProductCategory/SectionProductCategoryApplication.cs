using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.SectionProductCategory
{
    public class SectionProductCategoryApplication : ISectionProductCategoryApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISectionProductCategoryRepository _SectionProductCategoryRepository;

        public SectionProductCategoryApplication(ILogger logger, IServiceProvider serviceProvider, ISectionProductCategoryRepository sectionProductCategoryRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _SectionProductCategoryRepository=sectionProductCategoryRepository;
        }
    }
}
