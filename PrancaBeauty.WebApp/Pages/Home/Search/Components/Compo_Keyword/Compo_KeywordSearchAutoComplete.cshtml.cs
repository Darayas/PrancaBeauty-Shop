using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Keywords;
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
        private readonly IKeywordApplication _KeywordApplication;

        public Compo_KeywordSearchAutoCompleteModel(ILogger logger, IServiceProvider serviceProvider, IKeywordApplication keywordApplication)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _KeywordApplication=keywordApplication;
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
