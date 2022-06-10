using Framework.Domain.Contracts;
using Framework.Domain.Enums;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletWithdrawAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.BankAccount.BankAccountAgg.Entities
{
    public class tblBankAccounts : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public BankAccountNamesEnum BankName { get; set; }
        public string AccountNumber { get; set; } // شماره حساب
        public string CardNumber { get; set; } // شماره کارت
        public string IBAN { get; set; } // شماره شبا
        public string FullName { get; set; }
        public DateTime Date { get; set; } // تاریخ ثبت شماره حساب
        public bool IsConfirmed { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual ICollection<tblWalletWithdraw> tblWalletWithdraw { get; set; }
    }
}
