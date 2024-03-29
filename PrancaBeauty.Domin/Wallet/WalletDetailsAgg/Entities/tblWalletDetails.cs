﻿using Framework.Domain.Contracts;
using Framework.Domain.Enums;
using PrancaBeauty.Domin.Wallet.WalletAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletProductDepositDetailsAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletWithdrawAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Wallet.WalletDetailsAgg.Entities
{
    public class tblWalletDetails : IEntity
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public WalletDetailsTypeEnum DetailsType { get; set; }
        public string Data { get; set; }
        public DateTime Date { get; set; }

        public virtual tblWallets tblWallets { get; set; }
        public virtual ICollection<tblWalletProductDepositDetails> tblWalletProductDepositDetails { get; set; }
        public virtual ICollection<tblWalletWithdraw> tblWalletWithdraw { get; set; }
    }
}
