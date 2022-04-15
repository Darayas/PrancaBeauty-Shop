using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.SectionProductCategory
{
    public class SectionProductCategoryRepository : BaseRepository<tblSectionProductCategory>, ISectionProductCategoryRepository
    {
        public SectionProductCategoryRepository(MainContext Context) : base(Context)
        {

        }
    }
}
