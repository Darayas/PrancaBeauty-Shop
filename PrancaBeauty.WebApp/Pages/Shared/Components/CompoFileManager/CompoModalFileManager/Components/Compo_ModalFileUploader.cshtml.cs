using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Common.FtpWapper;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.CompoFileManager.CompoModalFileManager.Components
{
    public class Compo_ModalFileUploaderModel : PageModel
    {
        private readonly IFtpWapper _FtpWapper;
        private readonly ILocalizer _Localizer;

        public Compo_ModalFileUploaderModel(IFtpWapper ftpWapper, ILocalizer localizer)
        {
            _FtpWapper = ftpWapper;
            _Localizer = localizer;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Content(ModelState.GetErrors());

            if (!User.IsInRole(Roles.CanManageAllUserFiles))
                Input.UserId = User.GetUserDetails().UserId;

            var Result = await _FtpWapper.UploadFromFileManagerAsync(Input.Files, Input.UserId);
            if (Result.IsSucceeded)
                return Content("");
            else
                return StatusCode(500, _Localizer[Result.Message]);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ModalFileUploader Input { get; set; }
    }
}
