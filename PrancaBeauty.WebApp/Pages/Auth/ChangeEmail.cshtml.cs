using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth
{
    public class ChangeEmailModel : PageModel
    {
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        public ChangeEmailModel(IUserApplication userApplication, ILocalizer localizer)
        {
            _UserApplication = userApplication;
            _Localizer = localizer;
        }

        public async Task<IActionResult> OnGetAsync(string Token)
        {
            if (string.IsNullOrWhiteSpace(Token))
                return StatusCode(500);

            var Result = await _UserApplication.ChangeEmailAsync(Token);
            if (Result.IsSucceeded)
            {
                IsSuccess = true;
                Message = _Localizer[Result.Message];

                return Page();
            }
            else
            {
                IsSuccess = false;
                Message = _Localizer[Result.Message.Replace(", ", "<br/>")];

                return Page();
            }
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
