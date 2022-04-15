using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.SectionProductKeyword
{
    public class SectionProductKeywordRepository : BaseRepository<tblSectionProductKeyword>, ISectionProductKeywordRepository
    {
        public SectionProductKeywordRepository(MainContext Context) : base(Context)
        {

        }
    }
}
