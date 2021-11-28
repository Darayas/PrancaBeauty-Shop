using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Settings;
using PrancaBeauty.Domin.Settings.SettingsAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Settings
{
    public class SettingApplication : ISettingApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly ISettingRepository _SettingRepository;
        private List<OutSettings> _ListSettings;

        public SettingApplication(ISettingRepository settingRepository, ILogger logger, ILocalizer localizer)
        {
            _SettingRepository = settingRepository;
            _ListSettings = new List<OutSettings>();
            _Logger = logger;
            _Localizer = localizer;
        }

        public async Task<OutSettings> GetSettingAsync(InpGetSetting Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_Localizer);
                #endregion

                if (_ListSettings != null)
                    if (_ListSettings.Any(a => a.LangCode == Input.LangCode))
                        return _ListSettings.Where(a => a.LangCode == Input.LangCode).SingleOrDefault();

                var qSetting = await LoadSettingAsync(Input.LangCode);
                if (qSetting == null)
                {
                    // log
                    return null;
                }

                _ListSettings.Add(qSetting);
                return qSetting;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        private async Task<OutSettings> LoadSettingAsync(string LangCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LangCode))
                    throw new ArgumentNullException("LangCode cant be null.");

                var qData = await _SettingRepository.GetNoTraking
                                                    .Where(a => a.tblLanguages.Code == LangCode)
                                                    .Where(a => a.IsEnable == true)
                                                    .Select(a => new OutSettings
                                                    {
                                                        IsInManufacture = a.IsInManufacture,
                                                        LangCode = a.tblLanguages.Code,
                                                        SiteDescription = a.SiteDescription,
                                                        SiteEmail = a.SiteEmail,
                                                        SitePhoneNumber = a.SitePhoneNumber,
                                                        SiteTitle = a.SiteTitle,
                                                        SiteUrl = a.SiteUrl
                                                    })
                                                    .SingleOrDefaultAsync();

                if (qData == null)
                {
                    // throw new Exception($"qData is null, LangCode: [{LangCode}]");
                    return null;
                }



                return qData;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public void ClearCache()
        {
            _ListSettings = new List<OutSettings>();
        }
    }
}
