using Framework.Common.ExMethods;
using Framework.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.Products.Sellers
{
    [Authorize(Roles = Roles.CanViewDetailsProductSeller)]
    public class DetailsModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        public DetailsModel(IMsgBox msgBox, IServiceProvider serviceProvider)
        {
            _MsgBox = msgBox;
            _ServiceProvider = serviceProvider;
        }

        public async Task<IActionResult> OnGetAsync(viGetProductSellerDetails Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion



                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public List<vmProductSellerDetails> Data { get; set; }

        public string ProductId { get; set; }
    }
}
