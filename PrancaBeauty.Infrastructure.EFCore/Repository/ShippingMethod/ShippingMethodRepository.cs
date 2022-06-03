using Framework.Infrastructure;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Contracts;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ShippingMethod
{
    public class ShippingMethodRepository : BaseRepository<tblShippingMethods>, IShippingMethodRepository
    {
        public ShippingMethodRepository(MainContext Context) : base(Context)
        {

        }
    }
}
