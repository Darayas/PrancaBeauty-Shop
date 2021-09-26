using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewModel
{
    public class vmCompo_ModalFileList
    {
        public string Id { get; set; }
        public string FileTypeIconUrl { get; set; }
        public string Title { get; set; }
        public string MimeType { get; set; }
        public long FileSize { get; set; }
        public string DownloadLink { get; set; }
    }
}
