using Framework.Infrastructure;
using PrancaBeauty.Domin.Bills.TaxAgg.Contracts;
using PrancaBeauty.Domin.Bills.TaxAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.TaxGroup
{
    public class TaxGroupRepository : BaseRepository<tblTaxGroups>, ITaxGroupRepository
    {
        public TaxGroupRepository(MainContext Context) : base(Context)
        {

        }
    }
}
