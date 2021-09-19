using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Contracts.Templates;
using PrancaBeauty.Domin.Templates.TemplatesAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Templates
{
    public class TemplateApplication : ITemplateApplication
    {
        private readonly ISettingApplication _SettingApplication;
        private readonly ITemplateRepository _TemplateRepository;
        private List<OutTemplates> _ListTemplates;

        public TemplateApplication(ITemplateRepository templateRepository, ISettingApplication settingApplication)
        {
            _TemplateRepository = templateRepository;
            _ListTemplates = new List<OutTemplates>();
            _SettingApplication = settingApplication;
        }

        public async Task<string> GetEmailConfirmationTemplateAsync(string LangCode, string Url)
        {
            string _Template = await GetTemplateAsync(LangCode, "ConfirmationEmail");

            return (await SetGeneralParameters(_Template, LangCode))
                                                        .Replace("[Url]", Url);
        }

        public async Task<string> GetEmailChangeTemplateAsync(string LangCode, string Url)
        {
            string _Template = await GetTemplateAsync(LangCode, "ChanageEmail");

            return (await SetGeneralParameters(_Template, LangCode))
                                                        .Replace("[Url]", Url);
        }

        public async Task<string> GetEmailRecoveryPasswordTemplateAsync(string LangCode, string Url)
        {
            string _Template = await GetTemplateAsync(LangCode, "RecoveryPassword");

            return (await SetGeneralParameters(_Template, LangCode))
                                                        .Replace("[Url]", Url);
        }

        public async Task<string> GetEmailLoginTemplateAsync(string LangCode, string Url)
        {
            string _Template = await GetTemplateAsync(LangCode, "EmailLogin");

            return (await SetGeneralParameters(_Template, LangCode))
                                                        .Replace("[Url]", Url);
        }

        public async Task<string> GetTemplateAsync(string LangCode, string Name)
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
            var qSetting = await _SettingApplication.GetSettingAsync(LangCode);

            return Template.Replace("[SiteName]", qSetting.SiteTitle)
                           .Replace("[SiteUrl]", qSetting.SiteUrl);
        }

        public void ClearCache()
        {
            _ListTemplates = new List<OutTemplates>();
        }
    }
}
