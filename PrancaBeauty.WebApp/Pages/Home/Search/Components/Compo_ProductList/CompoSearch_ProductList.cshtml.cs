using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Apps.SearchHistory;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.SearchHistory;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Home.Search.Components.Compo_ProductList
{
    public class CompoSearch_ProductListModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductApplication _ProductApplication;
        private readonly ISearchHistoryApplication _SearchHistoryApplication;

        public CompoSearch_ProductListModel(ILogger logger, IServiceProvider serviceProvider, IProductApplication productApplication, IMapper mapper, ISearchHistoryApplication searchHistoryApplication)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ProductApplication=productApplication;
            _Mapper=mapper;

            Data= new vmCompoSearch_ProductList();
            _SearchHistoryApplication=searchHistoryApplication;
        }

        public async Task<IActionResult> OnGetAsync(string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get Data
                {
                    var _MappedData = _Mapper.Map<InpGetProductListForAdvanceSearch>(Input);
                    _MappedData.LangId=LangId;

                    var _Result = await _ProductApplication.GetProductListForAdvanceSearchAsync(_MappedData);

                    Data= _Mapper.Map<vmCompoSearch_ProductList>(_Result);
                }
                #endregion

                #region Set statistics
                {
                    await _SearchHistoryApplication.SetSearchStatisticsAsync(new InpSetSearchStatistics { LangId=LangId, KeywordTitle=Input.KeywordTitle });
                }
                #endregion

                return Page();
            }
            catch (ArgumentInvalidException)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompoSearch_ProductList Input { get; set; }
        public vmCompoSearch_ProductList Data { get; set; }
    }
}
