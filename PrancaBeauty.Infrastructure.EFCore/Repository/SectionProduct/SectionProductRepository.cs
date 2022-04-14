using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionProductAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionProductAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.SectionProduct
{
    public class SectionProductRepository : BaseRepository<tblSectionProducts>, ISectionProductRepository
    {
        public SectionProductRepository(MainContext Context) : base(Context)
        {

        }
    }
}
