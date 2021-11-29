using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Languages;
using PrancaBeauty.Domin.Region.LanguagesAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Languages
{
    public class LanguageApplication : ILanguageApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILanguageRepository _LanguageRepository;
        private List<OutSiteLangCache> SiteLangCache;

        public LanguageApplication(ILanguageRepository languageRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _LanguageRepository = languageRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<string> GetCodeByAbbrAsync(InpGetCodeByAbbr Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            await LoadCacheAsync();

            return SiteLangCache
                                .Where(a => a.Abbr == Input.Abbr)
                                .Select(a => a.Code)
                                .SingleOrDefault();
        }

        public async Task<string> GetAbbrByCodeAsync(InpGetAbbrByCode Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            await LoadCacheAsync();

            return SiteLangCache
                                .Where(a => a.Code == Input.Code)
                                .Select(a => a.Abbr)
                                .SingleOrDefault();
        }

        public async Task<string> GetNativeNameByCodeAsync(InpGetNativeNameByCode Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            await LoadCacheAsync();

            return SiteLangCache
                         .Where(a => a.Code == Input.Code)
                         .Select(a => a.NativeName)
                         .SingleOrDefault();
        }

        public async Task<string> GetFlagUrlByCodeAsync(InpGetFlagUrlByCode Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            await LoadCacheAsync();

            return SiteLangCache
                         .Where(a => a.Code == Input.Code)
                         .Select(a => a.FlagUrl)
                         .SingleOrDefault();
        }

        public async Task<string> GetDirectionByCodeAsync(InpGetDirectionByCode Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            await LoadCacheAsync();

            return SiteLangCache
                         .Where(a => a.Code == Input.Code)
                         .Select(a => a.IsRtl ? "rtl" : "ltr")
                         .SingleOrDefault();
        }

        private async Task LoadCacheAsync()
        {
            try
            {
                if (SiteLangCache == null)
                {
                    SiteLangCache = await _LanguageRepository.Get
                                                         .Where(a => a.IsActive)
                                                         .Where(a => a.UseForSiteLanguage)
                                                         .Select(a => new OutSiteLangCache
                                                         {
                                                             Id = a.Id.ToString(),
                                                             CountryId = a.CountryId.ToString(),
                                                             Abbr = a.Abbr,
                                                             Code = a.Code,
                                                             IsRtl = a.IsRtl,
                                                             Name = a.Name,
                                                             NativeName = a.NativeName,
                                                             FlagUrl = a.tblCountries.tblFiles.tblFilePaths.tblFileServer.HttpDomin +
                                                                        a.tblCountries.tblFiles.tblFilePaths.tblFileServer.HttpPath +
                                                                        a.tblCountries.tblFiles.tblFilePaths.Path +
                                                                        a.tblCountries.tblFiles.FileName
                                                         })
                                                         .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }

        public async Task<List<OutSiteLangCache>> GetAllLanguageForSiteLangAsync()
        {
            await LoadCacheAsync();

            return SiteLangCache;
        }

        public async Task<bool> IsValidAbbrForSiteLangAsync(InpIsValidAbbrForSiteLang Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            await LoadCacheAsync();

            return SiteLangCache
                        .Where(a => a.Abbr == Input.Abbr)
                        .Any();
        }

        public async Task<string> GetLangIdByLangCodeAsync(InpGetLangIdByLangCode Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            await LoadCacheAsync();

            return SiteLangCache
                         .Where(a => a.Code == Input.Code)
                         .Select(a => a.Id)
                         .SingleOrDefault();
        }

        public async Task<OutSiteLangCache> GetLangDetailsByIdAsync(InpGetLangDetailsById Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            return (await GetAllLanguageForSiteLangAsync()).Where(a => a.Id == Input.LangId).SingleOrDefault();
        }

        public async Task<string> GetCountryIdByLangIdAsync(InpGetCountryIdByLangId Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            await LoadCacheAsync();

            return SiteLangCache
                                .Where(a => a.Id == Input.LangId)
                                .Select(a => a.CountryId)
                                .SingleOrDefault();
        }
    }
}
