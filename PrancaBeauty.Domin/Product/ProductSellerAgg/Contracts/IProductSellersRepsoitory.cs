using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductSellerAgg.Contracts
{
    public interface IProductSellersRepsoitory : IRepository<tblProductSellers>
    {
    }
}
