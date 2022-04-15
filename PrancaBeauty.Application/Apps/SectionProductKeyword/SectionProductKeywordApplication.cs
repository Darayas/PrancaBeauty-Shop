using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.SectionProductKeyword
{
    public class SectionProductKeywordApplication: ISectionProductKeywordApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISectionProductKeywordRepository _SectionProductKeywordRepository;

        public SectionProductKeywordApplication(ILogger logger, IServiceProvider serviceProvider, ISectionProductKeywordRepository sectionProductKeywordRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _SectionProductKeywordRepository=sectionProductKeywordRepository;
        }
    }
}
