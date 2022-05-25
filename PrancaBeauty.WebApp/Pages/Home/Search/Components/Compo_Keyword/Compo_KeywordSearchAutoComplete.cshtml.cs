using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Keywords;
using PrancaBeauty.Application.Apps.SearchHistory;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Home.Search.Components.Compo_Keyword
{
    public class Compo_KeywordSearchAutoCompleteModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISearchHistoryApplication _SearchHistoryApplication;

        public Compo_KeywordSearchAutoCompleteModel(ILogger logger, IServiceProvider serviceProvider, ISearchHistoryApplication searchHistoryApplication)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _SearchHistoryApplication=searchHistoryApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                return Page();
            }
            catch (ArgumentInvalidException ex)
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
        public viCompo_KeywordSearchAutoComplete Input { get; set; }
        public vmCompo_KeywordSearchAutoComplete Data { get; set; }
    }
}
