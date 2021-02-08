using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Infrastructure.EFCore.Data;

namespace PrancaBeauty.WebApp.Pages.Home
{
    public class AddDataModel : PageModel
    {
        public void OnGet()
        {
            new AddData_Main().Run();
        }
    }
}
