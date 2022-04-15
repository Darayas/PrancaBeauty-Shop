using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.SectionFreeItem
{
    public class SectionFreeItemRepository : BaseRepository<tblSectionFreeItems>, ISectionFreeItemRepository
    {
        public SectionFreeItemRepository(MainContext Context) : base(Context)
        {

        }
    }
}
