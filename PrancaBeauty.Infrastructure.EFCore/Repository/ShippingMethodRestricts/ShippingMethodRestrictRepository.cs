using Framework.Infrastructure;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodRestrictAgg.Contracts;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ShippingMethodRestricts
{
    public class ShippingMethodRestrictRepository : BaseRepository<tblShippingMethods>, IShippingMethodRestrictRepository
    {
        public ShippingMethodRestrictRepository(MainContext Context) : base(Context)
        {

        }
    }
}
