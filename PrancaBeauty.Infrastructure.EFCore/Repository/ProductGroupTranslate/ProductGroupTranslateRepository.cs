using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductGroupTranslate
{
    public class ProductGroupTranslateRepository : BaseRepository<tblProductGroupTranslate>, IProductGroupTranslateRepository
    {
        public ProductGroupTranslateRepository(MainContext Context) : base(Context)
        {

        }
    }
}
