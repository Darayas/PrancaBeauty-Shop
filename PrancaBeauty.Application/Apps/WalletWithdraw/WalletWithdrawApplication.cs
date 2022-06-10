using Framework.Infrastructure;
using PrancaBeauty.Domin.Wallet.WalletWithdrawAgg.Contracts;
using System;

namespace PrancaBeauty.Application.Apps.WalletWithdraw
{
    public class WalletWithdrawApplication : IWalletWithdrawApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IWalletWithdrawRepository _WalletWithdrawRepository;

        public WalletWithdrawApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IWalletWithdrawRepository walletWithdrawRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _WalletWithdrawRepository=walletWithdrawRepository;
        }
    }
}
