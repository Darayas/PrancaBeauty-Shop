using Framework.Infrastructure;
using PrancaBeauty.Domin.Wallet.WalletProductDepositDetailsAgg.Contracts;
using PrancaBeauty.Domin.Wallet.WalletProductDepositDetailsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.WalletProductDepositDetails
{
    public class WalletProductDepositDetailsRepository : BaseRepository<tblWalletProductDepositDetails>, IWalletProductDepositDetailsRepository
    {
        public WalletProductDepositDetailsRepository(MainContext Context) : base(Context)
        {

        }
    }
}
