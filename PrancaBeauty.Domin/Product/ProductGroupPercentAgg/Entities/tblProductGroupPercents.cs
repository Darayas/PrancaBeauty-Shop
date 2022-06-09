using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Product.ProductGroupPercentAgg.Entities
{
    public class tblProductGroupPercents : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductGroupId { get; set; }
        public Guid WalletId { get; set; }
        public string Title { get; set; }
        public double Percent { get; set; }

        public virtual tblProductGroups tblProductGroups { get; set; }
        public virtual tblWallets tblWallets { get; set; }
    }
}
