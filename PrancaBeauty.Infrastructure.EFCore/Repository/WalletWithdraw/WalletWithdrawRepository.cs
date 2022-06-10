using Framework.Infrastructure;
using PrancaBeauty.Domin.Wallet.WalletWithdrawAgg.Contracts;
using PrancaBeauty.Domin.Wallet.WalletWithdrawAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.WalletWithdraw
{
    public class WalletWithdrawRepository : BaseRepository<tblWalletWithdraw>, IWalletWithdrawRepository
    {
        public WalletWithdrawRepository(MainContext Context) : base(Context)
        {

        }
    }
}
