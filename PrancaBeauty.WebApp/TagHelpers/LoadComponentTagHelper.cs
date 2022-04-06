using Framework.Common.Utilities.Downloader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.TagHelpers
{
    [HtmlTargetElement("LoadComponent")]
    public class LoadComponentTagHelper : TagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public HttpContext Context { get; set; }

        private readonly IDownloader _Downloader;
        private readonly ISettingApplication _SettingApplication;

        public LoadComponentTagHelper(IDownloader downloader, ISettingApplication settingApplication)
        {
            _Downloader = downloader;
            _SettingApplication = settingApplication;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Url))
                    throw new ArgumentNullException("Url cant be null.");

                if (Context == null)
                    throw new ArgumentNullException("Context cant be null.");

                Url = (await _SettingApplication.GetSettingAsync(new InpGetSetting {  LangCode= CultureInfo.CurrentCulture.Name })).SiteUrl + Url;

                string HtmlData = await _Downloader.GetHtmlFromPageAsync(Url, Data, Context.Request.Headers.Select(a => new KeyValuePair<string, string>(a.Key, a.Value)).ToDictionary(k => k.Key, v => v.Value));

                if (HtmlData == null)
                    throw new Exception("");

                output.TagName = "div";
                if (Id != null)
                    output.Attributes.SetAttribute("id", Id);

                if (Class != null)
                    output.Attributes.SetAttribute("class", Class);

                output.Content.SetHtmlContent(HtmlData);
            }
            catch (Exception)
            {
                output.TagName = "div";

                if (Id != null)
                    output.Attributes.SetAttribute("id", Id);

                if (Class != null)
                    output.Attributes.SetAttribute("class", Class);

                output.Content.SetHtmlContent("<err500>Module Error: 500, Internal Server Error</err500>");
            }
        }
    }
}
