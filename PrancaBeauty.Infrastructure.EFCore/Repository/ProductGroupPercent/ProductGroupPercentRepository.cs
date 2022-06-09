using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductGroupPercentAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductGroupPercentAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductGroupPercent
{
    public class ProductGroupPercentRepository : BaseRepository<tblProductGroupPercents>, IProductGroupPercentRepository
    {
        public ProductGroupPercentRepository(MainContext Context) : base(Context)
        {

        }
    }
}
