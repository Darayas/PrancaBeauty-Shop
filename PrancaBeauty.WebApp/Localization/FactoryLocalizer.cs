using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Localization
{
    public class FactoryLocalizer
    {
        public IStringLocalizer Set(IStringLocalizerFactory factory, Type TypeOfSharedResource)
        {
            var AssemblyName = new AssemblyName(TypeOfSharedResource.GetTypeInfo().Assembly.FullName);

            return factory.Create("SharedResource", AssemblyName.Name);
        }

        public IStringLocalizer Set(IServiceCollection services, Type TypeOfSharedResource)
        {
            var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
            var AssemblyName = new AssemblyName(TypeOfSharedResource.GetTypeInfo().Assembly.FullName);

            return factory.Create("SharedResource", AssemblyName.Name);
        }
    }
}
