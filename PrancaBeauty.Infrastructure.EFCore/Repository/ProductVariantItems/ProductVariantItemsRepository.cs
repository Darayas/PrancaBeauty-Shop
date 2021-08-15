using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductVariantItemsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductVariantItems
{
    public class ProductVariantItemsRepository : BaseRepository<tblProductVariantItems>, IProductVariantItemsRepository
    {
        public ProductVariantItemsRepository(MainContext Context) : base(Context)
        {

        }
    }
}
