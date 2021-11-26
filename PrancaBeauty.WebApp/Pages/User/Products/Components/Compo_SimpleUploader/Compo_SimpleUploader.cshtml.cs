using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.Application.Common.FtpWapper;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.Products.Components.Compo_SimpleUploader
{
    public class Compo_SimpleUploaderModel : PageModel
    {
        private readonly IFtpWapper _FtpWapper;
        private readonly ILocalizer _Localizer;
        private readonly IFileApplication _FileApplication;

        public Compo_SimpleUploaderModel(IFtpWapper ftpWapper, ILocalizer localizer, IFileApplication fileApplication)
        {
            _FtpWapper = ftpWapper;
            _Localizer = localizer;
            _FileApplication = fileApplication;
        }

        public async Task<JsonResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return new JsonResult(new { error = new { message = ModelState.GetErrors("\n\t") } });

            string _UserId = User.GetUserDetails().UserId;
            var Result = await _FtpWapper.UploadFromFileManagerAsync(new Application.Contracts.Common.FtpWapper.InpUploadFromFileManager { FormFile = Input.upload, UserId = _UserId });
            if (Result.IsSucceeded)
            {
                var FileUrl = await _FileApplication.GetFileUrlAsync(new Application.Contracts.Files.InpGetFileUrl { FileId = Result.Message });
                if (FileUrl == null)
                    return new JsonResult(new { error = new { message = _Localizer["PleaseTryAgain"] } });
                else
                    return new JsonResult(new { urls = new { @default = FileUrl } });
            }
            else
                return new JsonResult(new { error = new { message = _Localizer["Error500"] } });

        }

        [BindProperty]
        public viCompo_SimpleUploader Input { get; set; }
    }
}
