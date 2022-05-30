using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Categories;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Home.Search
{
    public class SearchModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ICategoryApplication _CategoryApplication;

        public SearchModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, ICategoryApplication categoryApplication)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _CategoryApplication=categoryApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get Topic id
                string TopicId = null;
                {
                    TopicId = await _CategoryApplication.GetTopicIdByCateNameAsync(new InpGetTopicIdByCateName
                    {
                        Name=Input.CategoryName
                    });

                }
                #endregion

                ViewData["TopicId"]=TopicId;

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
        public viSearch Input { get; set; }
        public vmSearch Data { get; set; }
    }
}
