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
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Admin.Categories
{
    [Authorize(Roles = Roles.CanEditCategory)]
    public class EditModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly ICategoryApplication _CategoryApplication;
        private readonly ILanguageApplication _LanguageApplication;
        public EditModel(ICategoryApplication categoryApplication, ILanguageApplication languageApplication, IMsgBox msgBox, IMapper mapper, ILocalizer localizer)
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

            var qData = await _CategoryApplication.GetForEditAsync(Input.Id);
            Input = _Mapper.Map<viEditCategory>(qData);

            var qLang = await _LanguageApplication.GetAllLanguageForSiteLangAsync();
            foreach (var item in qLang)
            {
                if (!Input.LstTranslate.Any(a => a.LangId == item.Id))
                    Input.LstTranslate.Add(new viEditCategory_Translate()
                    {
                        LangId = item.Id
                    });
            }

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viEditCategory Input { get; set; }
    }
}
