using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShowcaseTabSection
{
    public class ShowcaseTabSectionApplication: IShowcaseTabSectionApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseTabSectionRepository _ShowcaseTabSectionRepository;

        public ShowcaseTabSectionApplication(ILogger logger, IServiceProvider serviceProvider, IShowcaseTabSectionRepository showcaseTabSectionRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShowcaseTabSectionRepository=showcaseTabSectionRepository;
        }
    }
}
