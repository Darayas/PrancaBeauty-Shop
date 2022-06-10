using Framework.Infrastructure;
using System;

namespace PrancaBeauty.Application.Apps.BankAccount
{
    public class BankAccountApplication : IBankAccountApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;

        public BankAccountApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
        }
    }
}
