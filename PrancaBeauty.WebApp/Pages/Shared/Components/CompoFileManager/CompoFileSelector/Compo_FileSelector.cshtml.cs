using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.CompoFileManager.CompoFileSelector
{
    public class Compo_FileSelectorModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly IFileApplication _FileApplication;
        public Compo_FileSelectorModel(IMsgBox msgBox, IFileApplication fileApplication, IMapper mapper)
        {
            _MsgBox = msgBox;
            _FileApplication = fileApplication;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var qData = await _FileApplication.GetFileDetailsForFileSelectorAsync(Input.SelectedFilesId.Split(','));

            Data = _Mapper.Map<List<vmCompo_FileSelector>>(qData);

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_FileSelector Input { get; set; }
        public List<vmCompo_FileSelector> Data { get; set; }
    }
}
