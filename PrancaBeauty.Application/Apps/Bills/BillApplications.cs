using Framework.Infrastructure;
using PrancaBeauty.Application.Apps.Carts;
using PrancaBeauty.Domin.Bills.BillAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.Bills
{
    public class BillApplication : IBillApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IBillsRepository _BillRepository;
        private readonly ICartApplication _CartApplication;
        public BillApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, ICartApplication cartApplication)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _CartApplication=cartApplication;
        }


    }
}
