using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Categories;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Admin.Categories
{
    [Authorize(Roles = Roles.CanAddCategory)]
    public class AddModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly ICategoryApplication _CategoryApplication;
        private readonly ILanguageApplication _LanguageApplication;
        public AddModel(ICategoryApplication categoryApplication, ILanguageApplication languageApplication, IMsgBox msgBox, IMapper mapper, ILocalizer localizer)
        {
            _CategoryApplication = categoryApplication;
            _LanguageApplication = languageApplication;
            _MsgBox = msgBox;
            _Mapper = mapper;
            _Localizer = localizer;
        }

        public async Task<IActionResult> OnGetAsync(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/Category/List";

            var qLang = await _LanguageApplication.GetAllLanguageForSiteLangAsync();

            Input = new viAddCategory() { LstTranslate = new List<viAddCategory_Translate>() };

            foreach (var item in qLang)
            {
                Input.LstTranslate.Add(new viAddCategory_Translate()
                {
                    LangId = item.Id
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            Input.UserId = User.GetUserDetails().UserId;
            var Result = await _CategoryApplication.AddCategoryAsync(_Mapper.Map<InpAddCategory>(Input));
            if (Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "GotoList()");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty]
        public viAddCategory Input { get; set; }
    }
}
