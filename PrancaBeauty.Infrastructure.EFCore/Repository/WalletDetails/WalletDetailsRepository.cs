using Framework.Infrastructure;
using PrancaBeauty.Domin.Wallet.WalletDetailsAgg.Contracts;
using PrancaBeauty.Domin.Wallet.WalletDetailsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.WalletDetails
{
    public class WalletDetailsRepository : BaseRepository<tblWalletDetails>, IWalletDetailsRepository
    {
        public WalletDetailsRepository(MainContext Context) : base(Context)
        {

        }
    }
}
