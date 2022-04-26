using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Files;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.CompoFileManager.CompoFileSelector
{
    [Authorize]
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
            if (Input.SelectedFilesId == null)
                Input.SelectedFilesId = string.Empty;

            // تبدیل GUID به حروف کوچک برای جلوگیری از تداخل حروف کوچک و بزرگ
            Input.SelectedFilesId=Input.SelectedFilesId.ToLower();

            var qData = await _FileApplication.GetFileDetailsForFileSelectorAsync(Input.SelectedFilesId.Split(',').Select(a => new InpGetFileDetailsForFileSelector { FileId = a }).ToList());

            Data = _Mapper.Map<List<vmCompo_FileSelector>>(qData);

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_FileSelector Input { get; set; }
        public List<vmCompo_FileSelector> Data { get; set; }
    }
}
