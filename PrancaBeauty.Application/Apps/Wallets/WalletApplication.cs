using Framework.Infrastructure;
using PrancaBeauty.Domin.Wallet.WalletAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.Wallets
{
    public class WalletApplication : IWalletApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IWalletRepository _WalletRepository;

        public WalletApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IWalletRepository walletRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _WalletRepository=walletRepository;
        }
    }
}
