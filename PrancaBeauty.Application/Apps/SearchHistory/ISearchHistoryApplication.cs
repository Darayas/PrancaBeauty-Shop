using PrancaBeauty.Application.Contracts.ApplicationDTO.SearchHistory;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.SearchHistory
{
    public interface ISearchHistoryApplication
    {
        Task<OutGetDataForAutoComplete> GetDataForAutoCompleteAsync(InpGetDataForAutoComplete Input);
    }
}