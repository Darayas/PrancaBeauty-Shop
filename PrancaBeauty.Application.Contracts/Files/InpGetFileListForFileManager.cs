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
        public string UploaderUserId { get; set; }
        public InpGetFileListForFileManagerSort Sort { get; set; }
        public string FileTitle { get; set; }
        public int Take { get; set; }
        public int CurrentPage { get; set; }
    }

    public enum InpGetFileListForFileManagerSort
    {
        DateDes = 0,
        DateAes = 1,
        TitleAes = 2,
        TitleDes = 3,
        FileTypeAes = 4,
        FileTypeDes = 5,

    }

}
