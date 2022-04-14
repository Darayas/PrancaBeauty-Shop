using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ShowcaseTabs
{
    public class ShowcaseTabRepository : BaseRepository<tblShowcaseTabs>, IShowcaseTabsRepository
    {
        public ShowcaseTabRepository(MainContext Context) : base(Context)
        {

        }
    }
}
