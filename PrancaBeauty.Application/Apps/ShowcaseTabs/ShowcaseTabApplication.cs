using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.ShowcaseTabs
{
    public class ShowcaseTabApplication : IShowcaseTabApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseTabsRepository _ShowcaseTabsRepository;

        public ShowcaseTabApplication(ILogger logger, IServiceProvider serviceProvider, IShowcaseTabsRepository showcaseTabsRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShowcaseTabsRepository=showcaseTabsRepository;
        }
    }
}
