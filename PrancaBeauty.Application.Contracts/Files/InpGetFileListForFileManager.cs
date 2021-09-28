using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Files
{
    public class InpGetFileListForFileManager
    {
        public string FileTypeId { get; set; }
        public string UserId { get; set; }
        public InpGetFileListForFileManagerSort Sort { get; set; }
        public string Title { get; set; }
        public int Take { get; set; }
        public int Page { get; set; }
    }

    public enum InpGetFileListForFileManagerSort
    {
        TitleAes = 0,
        TitleDes = 1,
        FileTypeAes = 2,
        FileTypeDes = 3
    }

}
