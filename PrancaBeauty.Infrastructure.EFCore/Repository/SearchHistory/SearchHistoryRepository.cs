using Framework.Infrastructure;
using PrancaBeauty.Domin.Keywords.SearchHistoryAgg.Contracts;
using PrancaBeauty.Domin.Keywords.SearchHistoryAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.SearchHistory
{
    public class SearchHistoryRepository : BaseRepository<tblSearchHistory>, ISearchHistoryRepository
    {
        public SearchHistoryRepository(MainContext Context) : base(Context)
        {

        }
    }
}
