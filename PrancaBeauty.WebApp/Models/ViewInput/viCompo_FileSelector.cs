using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_FileSelector
    {
        public string FieldName { get; set; }
        public string SelectedFilesId { get; set; }
        public int MaxFileCount { get; set; }
        public string AllowedMimeType { get; set; }
        public long MaxFileSize { get; set; }
    }
}
