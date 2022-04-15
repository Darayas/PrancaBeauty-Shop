using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.SectionFreeItem
{
    public class SectionFreeItemApplication : ISectionFreeItemApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISectionFreeItemRepository _ISectionFreeItemRepository;

        public SectionFreeItemApplication(ILogger logger, IServiceProvider serviceProvider, ISectionFreeItemRepository SectionFreeItemRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ISectionFreeItemRepository=SectionFreeItemRepository;
        }
    }
}
