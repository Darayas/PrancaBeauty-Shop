using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ShowcaseTabSection
{
    public class ShowcaseTabSectionRepository : BaseRepository<tblShowcaseTabSections>, IShowcaseTabSectionRepository
    {
        public ShowcaseTabSectionRepository(MainContext Context) : base(Context)
        {

        }
    }
}
