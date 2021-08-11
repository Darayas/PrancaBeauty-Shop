using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductMediaAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductMediaAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductMedia
{
    public class ProductMediaRepository : BaseRepository<tblProductMedia>, IProductMediaRepository
    {

        public ProductMediaRepository(MainContext Context) : base(Context)
        {

        }
    }
}
