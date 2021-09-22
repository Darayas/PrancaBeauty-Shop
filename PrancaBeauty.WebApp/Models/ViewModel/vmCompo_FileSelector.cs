using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewModel
{
    public class vmCompo_FileSelector
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
