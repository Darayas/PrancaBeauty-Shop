using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.SearchHistory;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.SearchHistory
{
    public interface ISearchHistoryApplication
    {
        Task<OutGetWordDataForAutoComplete> GetWordDataForAutoCompleteAsync(InpGetWordDataForAutoComplete Input);
        Task<OperationResult> SetWordStatisticsAsync(InpSetWordStatistics Input);
    }
}