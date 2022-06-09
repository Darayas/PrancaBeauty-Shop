using Framework.Infrastructure;
using PrancaBeauty.Domin.Wallet.WalletAgg.Contracts;
using PrancaBeauty.Domin.Wallet.WalletAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Wallet
{
    public class WalletRepository : BaseRepository<tblWallets>, IWalletRepository
    {
        public WalletRepository(MainContext Context) : base(Context)
        {

        }
    }
}
