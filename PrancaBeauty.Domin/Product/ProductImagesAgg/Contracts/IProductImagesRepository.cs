using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductImagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductImagesAgg.Contracts
{
    public interface IProductImagesRepository : IRepository<tblProductImages>
    {
    }
}
