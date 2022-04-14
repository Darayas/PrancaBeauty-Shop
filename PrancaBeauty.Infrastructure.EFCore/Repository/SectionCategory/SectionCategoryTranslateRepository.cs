using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionCategoryAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionCategoryAgg.Entities;
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
