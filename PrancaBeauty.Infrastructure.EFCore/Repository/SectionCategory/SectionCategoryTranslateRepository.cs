using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionCategories.Contracts;
using PrancaBeauty.Domin.Showcases.SectionCategories.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.SectionCategory
{
    public class SectionCategoryTranslateRepository : BaseRepository<tblSectionCategoryTranslate>, ISectionCategoryTranslateRepository
    {
        public SectionCategoryTranslateRepository(MainContext Context) : base(Context)
        {

        }
    }
}
