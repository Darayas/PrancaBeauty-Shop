using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Common.Utilities.Downloader
{
    public interface IDownloader
    {
        Task<string> GetHtmlFromPageAsync(string PageUrl, object Data, Dictionary<string, string> Headers);
    }
}