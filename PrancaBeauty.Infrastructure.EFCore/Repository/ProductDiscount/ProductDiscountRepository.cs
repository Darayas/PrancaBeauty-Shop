using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductDiscountAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductDiscountAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductDiscount
{
    public class ProductDiscountRepository : BaseRepository<tblProductDiscounts>, IProductDiscountRepository
    {
        public ProductDiscountRepository(MainContext Context) : base(Context)
        {

        }
    }

}
