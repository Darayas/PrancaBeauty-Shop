using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.FileTypes;
using PrancaBeauty.Application.Contracts.FileTypes;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Combo_FileType
{
    [Authorize]
    public class Compo_ComboFileTypesModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IFileTypeApplication _FileTypeApplication;

        public Compo_ComboFileTypesModel(IFileTypeApplication fileTypeApplication, IMapper mapper)
        {
            _FileTypeApplication = fileTypeApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.FileTypeId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string Text)
        {
            var qData = await _FileTypeApplication.GetListForComboAsync(new InpGetListForCombo { Title = Text });
            var Data = _Mapper.Map<List<vmCompo_ComboFileTypes>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ComboFileTypes Input { get; set; }
    }
}
