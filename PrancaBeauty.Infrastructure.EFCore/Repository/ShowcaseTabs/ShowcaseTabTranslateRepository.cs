using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ShowcaseTabs
{
    public class ShowcaseTabTranslateRepository : BaseRepository<tblShowcaseTabTranslates>, IShowcaseTabsTranslateRepository
    {
        public ShowcaseTabTranslateRepository(MainContext Context) : base(Context)
        {

        }
    }
}
