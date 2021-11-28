using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Contracts.Settings;
using PrancaBeauty.Application.Contracts.Templates;
using PrancaBeauty.Domin.Templates.TemplatesAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Templates
{
    public class TemplateApplication : ITemplateApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly ISettingApplication _SettingApplication;
        private readonly ITemplateRepository _TemplateRepository;
        private List<OutTemplates> _ListTemplates;

        public TemplateApplication(ITemplateRepository templateRepository, ISettingApplication settingApplication, ILogger logger, ILocalizer localizer)
        {
            _TemplateRepository = templateRepository;
            _ListTemplates = new List<OutTemplates>();
            _SettingApplication = settingApplication;
            _Logger = logger;
            _Localizer = localizer;
        }

        public async Task<string> GetEmailConfirmationTemplateAsync(InpGetEmailConfirmationTemplate Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_Localizer);
                #endregion

                string _Template = await GetTemplateAsync(Input.LangCode, "ConfirmationEmail");

                return (await SetGeneralParameters(_Template, Input.LangCode))
                                                            .Replace("[Url]", Input.Url);
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

        public async Task<string> GetEmailChangeTemplateAsync(InpGetEmailChangeTemplate Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_Localizer);
                #endregion

                string _Template = await GetTemplateAsync(Input.LangCode, "ChanageEmail");

                return (await SetGeneralParameters(_Template, Input.LangCode))
                                                            .Replace("[Url]", Input.Url);
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

        public async Task<string> GetEmailRecoveryPasswordTemplateAsync(InpGetEmailRecoveryPasswordTemplate Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_Localizer);
                #endregion

                string _Template = await GetTemplateAsync(Input.LangCode, "RecoveryPassword");

                return (await SetGeneralParameters(_Template, Input.LangCode))
                                                            .Replace("[Url]", Input.Url);
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

        public async Task<string> GetEmailLoginTemplateAsync(InpGetEmailLoginTemplate Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_Localizer);
                #endregion

                string _Template = await GetTemplateAsync(Input.LangCode, "EmailLogin");

                return (await SetGeneralParameters(_Template, Input.LangCode))
                                                            .Replace("[Url]", Input.Url);
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

        private async Task<string> GetTemplateAsync(string LangCode, string Name)
        {
            if (_ListTemplates != null)
                if (_ListTemplates.Where(a => a.Name == Name && a.LangCode == LangCode).Any())
                    return _ListTemplates.Where(a => a.Name == Name && a.LangCode == LangCode).Select(a => a.Template).Single();

            string _Template = await _TemplateRepository.GetNoTraking
                                                        .Where(a => a.Name == Name && a.tblLanguages.Code == LangCode)
                                                        .Select(a => a.Template)
                                                        .SingleAsync();

            _ListTemplates.Add(new OutTemplates
            {
                Name = Name,
                LangCode = LangCode,
                Template = _Template
            }); ;

            return _Template;
        }

        private async Task<string> SetGeneralParameters(string Template, string LangCode)
        {
            var qSetting = await _SettingApplication.GetSettingAsync(new InpGetSetting { LangCode = LangCode });

            return Template.Replace("[SiteName]", qSetting.SiteTitle)
                           .Replace("[SiteUrl]", qSetting.SiteUrl);
        }

        public void ClearCache()
        {
            _ListTemplates = new List<OutTemplates>();
        }
    }
}
