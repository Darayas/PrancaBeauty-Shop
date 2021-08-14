using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductVariants
{
    public class ProductVariantsRepository : BaseRepository<tblProductVariants>, IProductVariantsRepository
    {
        public ProductVariantsRepository(MainContext Context) : base(Context)
        {

        }
    }
}
