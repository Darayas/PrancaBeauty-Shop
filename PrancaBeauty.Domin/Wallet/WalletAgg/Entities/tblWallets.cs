using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductGroupPercentAgg.Entities;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletDetailsAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Wallet.WalletAgg.Entities
{
    public class tblWallets : IEntity
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid CurrencyId { get; set; }
        public string Title { get; set; }
        public string Data { get; set; }


        public virtual tblUsers tblUsers { get; set; }
        public virtual tblCurrencies tblCurrencies { get; set; }
        public virtual ICollection<tblProductGroupPercents> tblProductGroupPercents { get; set; }
        public virtual ICollection<tblWalletDetails> tblWalletDetails { get; set; }
    }
}
