using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Categories;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Keywords;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.SearchHistory;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class SearchHistoryMapping : Profile
    {
        public SearchHistoryMapping()
        {
            CreateMap<OutGetProductsByTitleForSearchAutoComplete, OutGetDataForAutoComplete_Products>();
            CreateMap<OutGetCategoriesForSeachAutoComplete, OutGetDataForAutoComplete_RelatedCategories>();
            CreateMap<OutGetKeywordForSearchAutoComplete, OutGetDataForAutoComplete_RelatedKeywords>();

            CreateMap<OutGetDataForAutoComplete, vmCompo_KeywordSearchAutoComplete>();
            CreateMap<OutGetDataForAutoComplete_RelatedWords, vmCompo_KeywordSearchAutoComplete_RelatedWords>();
            CreateMap<OutGetDataForAutoComplete_RelatedKeywords, vmCompo_KeywordSearchAutoComplete_RelatedKeywords>();
            CreateMap<OutGetDataForAutoComplete_RelatedCategories, vmCompo_KeywordSearchAutoComplete_RelatedCategories>();
            CreateMap<OutGetDataForAutoComplete_Products, vmCompo_KeywordSearchAutoComplete_Products>();
        }
    }
}
