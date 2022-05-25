using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.SearchHistory;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class SearchHistoryMapping : Profile
    {
        public SearchHistoryMapping()
        {
            CreateMap<OutGetProductsByTitleForSearchAutoComplete, OutGetDataForAutoComplete_Products>();
        }
    }
}
