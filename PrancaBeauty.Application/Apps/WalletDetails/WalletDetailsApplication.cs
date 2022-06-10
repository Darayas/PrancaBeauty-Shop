using Framework.Infrastructure;
using PrancaBeauty.Domin.Wallet.WalletDetailsAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.WalletDetails
{
    public class WalletDetailsApplication : IWalletDetailsApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IWalletDetailsRepository _WalletDetailsRepository;

        public WalletDetailsApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IWalletDetailsRepository walletDetailsRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _WalletDetailsRepository=walletDetailsRepository;
        }
    }
}
