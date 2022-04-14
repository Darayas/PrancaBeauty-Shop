using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionCategories.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.SectionCategory
{
    public class SectionCategoryApplication : ISectionCategoryApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISectionCategoryRepository _SectionCategoryRepository;

        public SectionCategoryApplication(ILogger logger, IServiceProvider serviceProvider, ISectionCategoryRepository sectionCategoryRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _SectionCategoryRepository=sectionCategoryRepository;
        }
    }
}
