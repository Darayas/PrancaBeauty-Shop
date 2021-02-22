using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
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

        public LoadComponentTagHelper()
        {

        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string HtmlData = "";

            if (HtmlData == null)
                throw new Exception("");

            output.TagName = "div";
            if (Id != null)
                output.Attributes.SetAttribute("id", Id);

            if (Class != null)
                output.Attributes.SetAttribute("class", Class);

            output.Content.SetHtmlContent(HtmlData);
        }
    }
}
