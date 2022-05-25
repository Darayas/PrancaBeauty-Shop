using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.Application.Apps.Keywords;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Categories;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Keywords;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.SearchHistory;
using PrancaBeauty.Domin.Keywords.SearchHistoryAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.SearchHistory
{
    public class SearchHistoryApplication : ISearchHistoryApplication
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISearchHistoryRepository _SearchHistoryRepository;
        private readonly IProductApplication _ProductApplication;
        private readonly ICategoryApplication _CategoryApplication;
        private readonly IKeywordApplication _KeywordApplication;

        public SearchHistoryApplication(ILogger logger, IServiceProvider serviceProvider, ISearchHistoryRepository searchHistoryRepository, IProductApplication productApplication, ICategoryApplication categoryApplication, IKeywordApplication keywordApplication, IMapper mapper)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _SearchHistoryRepository=searchHistoryRepository;
            _ProductApplication=productApplication;
            _CategoryApplication=categoryApplication;
            _KeywordApplication=keywordApplication;
            _Mapper=mapper;
        }

        public async Task<OutGetDataForAutoComplete> GetDataForAutoCompleteAsync(InpGetDataForAutoComplete Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = new OutGetDataForAutoComplete();

                #region Get products
                {
                    var qProducts = await _ProductApplication.GetProductsByTitleForSearchAutoCompleteAsync(new InpGetProductsByTitleForSearchAutoComplete
                    {
                        Title=Input.KeywordTitle,
                        Take=5
                    });
                    if (qProducts!=null)
                        qData.LstProducts= _Mapper.Map<List<OutGetDataForAutoComplete_Products>>(qProducts);
                }
                #endregion

                #region Get Categoris
                {
                    var qCategories = await _CategoryApplication.GetCategoriesForSeachAutoCompleteAsync(new InpGetCategoriesForSeachAutoComplete
                    {
                        Title=Input.KeywordTitle,
                        LangId=Input.LangId
                    });
                    if (qCategories!=null)
                        qData.LstRelatedCategory= _Mapper.Map<List<OutGetDataForAutoComplete_RelatedCategories>>(qCategories);
                }
                #endregion

                #region Get Keywords
                {
                    var qKeywords = await _KeywordApplication.GetKeywordForSearchAutoCompleteAsync(new InpGetKeywordForSearchAutoComplete
                    {
                        Title= Input.KeywordTitle
                    });
                    if (qKeywords!=null)
                        qData.LstRelatedKeywords= _Mapper.Map<List<OutGetDataForAutoComplete_RelatedKeywords>>(qKeywords);
                }
                #endregion

                #region Get Words
                {
                    var qWords = await _SearchHistoryRepository.Get
                                            .Where(a => a.LangId==Input.LangId.ToGuid())
                                            .Where(a => a.Title.Contains(Input.KeywordTitle))
                                            .OrderByDescending(a => a.CountSearch)
                                            .Select(a => new OutGetDataForAutoComplete_RelatedWords
                                            {
                                                Id=a.Id.ToString(),
                                                Title=a.Title
                                            })
                                            .Take(5)
                                            .ToListAsync();
                }
                #endregion

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }
    }
}
