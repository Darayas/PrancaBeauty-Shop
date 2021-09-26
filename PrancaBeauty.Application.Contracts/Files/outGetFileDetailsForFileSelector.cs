using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Files
{
    public class outGetFileDetailsForFileSelector
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public long FileSize { get; set; }
        public string FileServerName { get; set; }
        public string DownloadUrl { get; set; }
        public string FileTypeIconUrl { get; set; }
    }
}
