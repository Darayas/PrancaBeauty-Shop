﻿using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletDetailsAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Wallet.WalletDepositDetailsAgg.Entities
{
    public class tblWalletDepositDetails : IEntity
    {
        public Guid Id { get; set; }
        public Guid WalletDetailsId { get; set; }
        public Guid ProductId { get; set; }
        public Guid VariantItemId { get; set; }

        public virtual tblWalletDetails tblWalletDetails { get; set; }
        public virtual tblProducts tblProducts { get; set; }
        public virtual tblProductVariantItems tblProductVariantItems { get; set; }
    }
}
