using Framework.Infrastructure;
using Microsoft.Extensions.Localization;
using PrancaBeauty.WebApp.Localization.Resource;

namespace PrancaBeauty.WebApp.Localization
{
    public class Localizer : ILocalizer
    {
        public string this[string Name] => Get(Name);

        public string this[string Name, params object[] argumets] => Get(Name, argumets);

        private readonly IStringLocalizer _SharedLocalizer;

        public Localizer(IStringLocalizerFactory stringLocalizerFactory)
        {
            _SharedLocalizer = new FactoryLocalizer().Set(stringLocalizerFactory, typeof(SharedResource));
        }

        public Localizer(IStringLocalizer stringLocalizer)
        {
            _SharedLocalizer = stringLocalizer;
        }

        private string Get(string Name)
        {
            return _SharedLocalizer[Name];
        }

        private string Get(string Name, params object[] argumets)
        {
            return _SharedLocalizer[Name, argumets];
        }
    }
}
