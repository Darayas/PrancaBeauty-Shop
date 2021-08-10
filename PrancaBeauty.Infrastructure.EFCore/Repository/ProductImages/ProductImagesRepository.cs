using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductImagesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductImagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductImages
{
    public class ProductImagesRepository : BaseRepository<tblProductImages>, IProductImagesRepository
    {

        public ProductImagesRepository(MainContext Context) : base(Context)
        {

        }
    }
}
