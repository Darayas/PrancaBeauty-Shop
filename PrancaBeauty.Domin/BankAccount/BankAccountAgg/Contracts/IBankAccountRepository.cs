using Framework.Domain.Contracts;
using PrancaBeauty.Domin.BankAccount.BankAccountAgg.Entities;

namespace PrancaBeauty.Domin.BankAccount.BankAccountAgg.Contracts
{
    public interface IBankAccountRepository : IRepository<tblBankAccounts>
    {
    }
}
