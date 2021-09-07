using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Settings;
using PrancaBeauty.Domin.Settings.SettingsAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Settings
{
    public class SettingApplication : ISettingApplication
    {
        private readonly ILogger _Logger;
        private readonly ISettingRepository _SettingRepository;
        private List<OutSettings> _ListSettings;

        public SettingApplication(ISettingRepository settingRepository, ILogger logger)
        {
            _SettingRepository = settingRepository;
            _ListSettings = new List<OutSettings>();
            _Logger = logger;
        }

        public async Task<OutSettings> GetSettingAsync(string LangCode)
        {
            if (_ListSettings != null)
                if (_ListSettings.Any(a => a.LangCode == LangCode))
                    return _ListSettings.Where(a => a.LangCode == LangCode).SingleOrDefault();

            var qSetting = await LoadSettingAsync(LangCode);
            if (qSetting == null)
            {
                // log
                return null;
            }

            _ListSettings.Add(qSetting);
            return qSetting;
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
