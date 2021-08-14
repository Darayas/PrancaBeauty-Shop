using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductVariantAgg.Contracts
{
    public interface IProductVariantsRepository : IRepository<tblProductVariants>
    {
    }
}
