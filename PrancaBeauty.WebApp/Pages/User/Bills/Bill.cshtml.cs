using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Bills;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.Bills
{
    public class BillModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IBillApplication _BillApplication;

        public BillModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IBillApplication billApplication)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _BillApplication=billApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viBill Input { get; set; }
        public vmBill Data { get; set; }
    }
}
