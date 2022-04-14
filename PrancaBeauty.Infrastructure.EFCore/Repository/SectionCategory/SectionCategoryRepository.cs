using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionCategoryAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionCategoryAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.SectionCategory
{
    public class SectionCategoryRepository : BaseRepository<tblSectionCategories>, ISectionCategoryRepository
    {
        public SectionCategoryRepository(MainContext Context) : base(Context)
        {

        }
    }
}
