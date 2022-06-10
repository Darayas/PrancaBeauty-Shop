using Framework.Infrastructure;
using PrancaBeauty.Domin.BankAccount.BankAccountAgg.Contracts;
using PrancaBeauty.Domin.BankAccount.BankAccountAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.BankAccounts
{
    public class BankAccountRepository : BaseRepository<tblBankAccounts>, IBankAccountRepository
    {
        public BankAccountRepository(MainContext Context) : base(Context)
        {

        }
    }
}
