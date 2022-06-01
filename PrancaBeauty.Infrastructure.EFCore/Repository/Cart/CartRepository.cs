using Framework.Infrastructure;
using PrancaBeauty.Domin.Cart.CartAgg.Contracts;
using PrancaBeauty.Domin.Cart.CartAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Cart
{
    public class CartRepository : BaseRepository<tblCarts>, ICartRepository
    {
        public CartRepository(MainContext Context) : base(Context)
        {

        }
    }
}
