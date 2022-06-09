using Framework.Infrastructure;
using PrancaBeauty.Domin.Bills.TaxAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.TaxGroups
{
    public class TaxGroupApplication : ITaxGroupApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ITaxGroupRepository _TaxGroupRepository;

        public TaxGroupApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, ITaxGroupRepository taxGroupRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _TaxGroupRepository=taxGroupRepository;
        }
    }
}
