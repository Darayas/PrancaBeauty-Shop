using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionItems.Contracts;
using PrancaBeauty.Domin.Showcases.SectionItems.Entitiy;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.SectionItems
{
    public class ShowcaseTabSectionItemRepository : BaseRepository<tblShowcaseTabSectionItems>, IShowcaseTabSectionItemRepository
    {
        public ShowcaseTabSectionItemRepository(MainContext Context) : base(Context)
        {

        }
    }
}
