using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Bills;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.Bills
{
    [Authorize]
    public class ListBillsModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IBillApplication _BillApplication;

        public ListBillsModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IBillApplication billApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _BillApplication=billApplication;
            _MsgBox=msgBox;
            _Localizer=localizer;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostCreateBillAsync(viCreateBill Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                string _UserId = User.GetUserDetails().UserId;


                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }
    }
}
