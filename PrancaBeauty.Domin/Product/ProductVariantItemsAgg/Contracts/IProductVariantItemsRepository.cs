using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductVariantItemsAgg.Contracts
{
    public interface IProductVariantItemsRepository : IRepository<tblProductVariantItems>
    {
    }
}
