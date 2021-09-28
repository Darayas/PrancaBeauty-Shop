using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Files
{
    public class OutGetFileListForFileManager
    {
        public string Id { get; set; }
        public string FileTypeIconUrl { get; set; }
        public string Title { get; set; }
        public string MimeType { get; set; }
        public long FileSize { get; set; }
        public string DownloadLink { get; set; }
        public DateTime Date { get; set; }
    }
}
