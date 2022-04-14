using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionCategories.Contracts;
using PrancaBeauty.Domin.Showcases.SectionCategories.Entities;
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
