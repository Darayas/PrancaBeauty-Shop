using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionItems.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.SectionItems
{
    public class ShowcaseTabSectionItemApplication : IShowcaseTabSectionItemApplication
    {
        private readonly ILogger _Logger;
        public readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IShowcaseTabSectionItemRepository _ShowcaseTabSectionItemRepository;

        public ShowcaseTabSectionItemApplication(ILogger Logger, IServiceProvider ServiceProvider, ILocalizer Localizer, IShowcaseTabSectionItemRepository ShowcaseTabSectionItemRepository)
        {
            _Logger= Logger;
            _ServiceProvider= ServiceProvider;
            _Localizer= Localizer;
            _ShowcaseTabSectionItemRepository= ShowcaseTabSectionItemRepository;
        }


    }
}
