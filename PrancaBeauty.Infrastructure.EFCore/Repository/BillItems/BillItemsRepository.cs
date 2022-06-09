using Framework.Infrastructure;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Contracts;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.BillItems
{
    public class BillItemsRepository : BaseRepository<tblBillItems>, IBillItemsRepository
    {
        public BillItemsRepository(MainContext Context) : base(Context)
        {

        }
    }
}
