using Framework.Infrastructure;
using System;

namespace PrancaBeauty.Application.Apps.Bills
{
    public class BillApplication : IBillApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;

        public BillApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
        }
    }
}
