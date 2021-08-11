using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductAskLikes
{
    public class ProductAskLikesRepository : BaseRepository<tblProductAskLikes>, IProductAskLikesRepository
    {
        public ProductAskLikesRepository(MainContext Context) : base(Context)
        {

        }
    }
}
