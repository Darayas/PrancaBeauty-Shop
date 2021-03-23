using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Languages;
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
        private List<OutSiteLangCache> SiteLangCache;

        public LanguageApplication(ILanguageRepository languageRepository)
        {
            _LanguageRepository = languageRepository;
        }

        public async Task<string> GetCodeByAbbrAsync(string Abbr)
        {
            await LoadCacheAsync();

            return SiteLangCache
                                .Where(a => a.Abbr == Abbr)
                                .Select(a => a.Code)
                                .SingleOrDefault();
        }

        private async Task LoadCacheAsync()
        {
            if (SiteLangCache == null)
            {
                SiteLangCache = await _LanguageRepository.Get
                                                     .Where(a => a.IsActive)
                                                     .Where(a => a.UseForSiteLanguage)
                                                     .Select(a => new OutSiteLangCache
                                                     {
                                                         Id = a.Id.ToString(),
                                                         Abbr = a.Abbr,
                                                         Code = a.Code,
                                                         IsRtl = a.IsRtl,
                                                         Name = a.Name,
                                                         NativeName = a.NativeName
                                                     })
                                                     .ToListAsync();
            }

        }

        public async Task<List<OutSiteLangCache>> GetAllLanguageForSiteLangAsync()
        {
            await LoadCacheAsync();

            return SiteLangCache;
        }
    }
}
