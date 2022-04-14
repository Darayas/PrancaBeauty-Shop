using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.Showcases
{
    public class ShowcaseApplication : IShowcaseApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseRepository _ShowcaseRepository;

        public ShowcaseApplication(ILogger logger, IServiceProvider serviceProvider, IShowcaseRepository showcaseRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShowcaseRepository=showcaseRepository;
        }
    }
}
