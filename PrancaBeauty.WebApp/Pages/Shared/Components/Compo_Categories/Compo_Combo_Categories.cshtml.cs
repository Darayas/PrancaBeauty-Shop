using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_Categories
{
    public class Compo_Combo_CategoriesModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly ICategoryApplication _CategoryApplication;

        public Compo_Combo_CategoriesModel(ICategoryApplication categoryApplication, IMapper mapper)
        {
            _CategoryApplication = categoryApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.CategoryId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string LangId, string id)
        {
            var qData = await _CategoryApplication.GetListForComboAsync(LangId, id);
            //var Data = _Mapper.Map<List<vmCompo_Combo_Countries>>(qData);
            //return new JsonResult(Data);
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Categories Input { get; set; }
    }
}
