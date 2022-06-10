using Framework.Domain.Contracts;
using Framework.Domain.Enums;
using PrancaBeauty.Domin.BankAccount.BankAccountAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletDetailsAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Wallet.WalletWithdrawAgg.Entities
{
    public class tblWalletWithdraw : IEntity
    {
        public Guid Id { get; set; }
        public Guid WalletDetailsId { get; set; }
        public Guid BankAccountId { get; set; }
        public WalletWithdrawTypeEnum Type { get; set; }
        public string Data { get; set; }
        public DateTime Date { get; set; }

        public virtual tblWalletDetails tblWalletDetails { get; set; }
        public virtual tblBankAccounts tblBankAccounts { get; set; }
    }
}
