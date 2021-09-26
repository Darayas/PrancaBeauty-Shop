using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ModalFileList
    {
        public string FieldName { get; set; }
        public string FileTypeId { get; set; }
        public string UserId { get; set; }
        public string Sort { get; set; }

        public string AllowedMimeType { get; set; }
        public long MaxFileSize { get; set; }
    }
}
