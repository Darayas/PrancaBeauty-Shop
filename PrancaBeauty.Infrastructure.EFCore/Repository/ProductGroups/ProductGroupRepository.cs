using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductGroups
{
    public class ProductGroupRepository : BaseRepository<tblProductGroups>, IProductGroupRepository
    {
        public ProductGroupRepository(MainContext Context) : base(Context)
        {

        }
    }
}
