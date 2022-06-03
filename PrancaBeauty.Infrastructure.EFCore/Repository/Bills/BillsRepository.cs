using Framework.Infrastructure;
using PrancaBeauty.Domin.Bills.BillAgg.Contracts;
using PrancaBeauty.Domin.Bills.BillAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Bills
{
    public class BillsRepository : BaseRepository<tblBills>, IBillsRepository
    {
        public BillsRepository(MainContext Context) : base(Context)
        {

        }
    }
}
