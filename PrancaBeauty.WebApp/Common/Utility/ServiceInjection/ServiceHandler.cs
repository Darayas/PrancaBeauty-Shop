using Framework.Common.Utilities.ServiceInjection;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.Utility.ServiceInjection
{
    public class ServiceHandler : IServiceHandler
    {
        private readonly IHttpContextAccessor _HttpContext;
        public ServiceHandler(IHttpContextAccessor httpContext)
        {
            _HttpContext = httpContext;
        }

        public T GetService<T>()
        {
            return _HttpContext.HttpContext.GetService<T>();
        }
    }
}
