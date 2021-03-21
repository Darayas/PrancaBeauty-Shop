using PrancaBeauty.Domin.Region.LanguagesAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Languages
{
    public class LanguageApplication : ILanguageApplication
    {
        private readonly ILanguageRepository _LanguageRepository;
        public LanguageApplication(ILanguageRepository languageRepository)
        {
            _LanguageRepository = languageRepository;
        }


    }
}
