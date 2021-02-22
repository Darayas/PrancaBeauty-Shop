using Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Utilities.Downloader
{
    public class Downloader : IDownloader
    {
        private readonly ILogger _Logger;
        public Downloader(ILogger logger)
        {
            _Logger = logger;
        }
        public async Task<string> GetHtmlFromPageAsync(string PageUrl, object Data, Dictionary<string, string> Headers)
        {
            try
            {
                string UrlParemeter = "";

                string Url = PageUrl + UrlParemeter.Trim(new char[] { '&' });

                HttpWebRequest ObjRequest = (HttpWebRequest)HttpWebRequest.Create(Url);

                ObjRequest.ContentType = "text/html; charset=utf-8";
                ObjRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; InfoPath.1; .NET CLR 2.0.50727; .NET CLR 1.1.4322)";

                #region افزودن هدر ها
                if (Headers != null)
                    foreach (var item in Headers)
                    {
                        ObjRequest.Headers.Add(item.Key, item.Value);
                    }
                #endregion

                ObjRequest.Headers.Add("Accept-charset", "ISO-8859-9,URF-8;q=0.7,*;q=0.7");
                ObjRequest.Headers["Accept-Encoding"] = "deflate";

                var Response = (HttpWebResponse)(await ObjRequest.GetResponseAsync());

                StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);

                string Result = await sr.ReadToEndAsync();

                sr.Close();

                return Result;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
