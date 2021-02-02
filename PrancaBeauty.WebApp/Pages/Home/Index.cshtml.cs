using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Infrastructure.Logger.Contracts;
using PrancaBeauty.WebApp.Localization;

namespace PrancaBeauty.WebApp.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _Logger;
        public IndexModel(ILogger Logger)
        {
            _Logger = Logger;
        }
        public void OnGet()
        {
            try
            {
                _Logger.Information("Start");

                int a = 5;
                int b = 0;
                int c = a / b;

            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                throw;
            }
            finally
            {
                _Logger.Information("End");
            }
        }
    }
}
