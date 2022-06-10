using Framework.Infrastructure;
using PrancaBeauty.Domin.Wallet.WalletProductDepositDetailsAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.WalletProductDepositDetails
{
    public class WalletProductDepositDetailsApplication : IWalletProductDepositDetailsApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IWalletProductDepositDetailsRepository _WalletProductDepositDetailsRepository;

        public WalletProductDepositDetailsApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IWalletProductDepositDetailsRepository walletProductDepositDetailsRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _WalletProductDepositDetailsRepository=walletProductDepositDetailsRepository;
        }
    }
}
