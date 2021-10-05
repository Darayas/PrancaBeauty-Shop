using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ModalFileList
    {
        public string FieldName { get; set; }
        public string FileTitle { get; set; }
        public string FileTypeId { get; set; }
        public string UploaderUserId { get; set; }
        public viCompo_ModalFileListSort Sort { get; set; }

        public int CurrentPage { get; set; }
        public int Take { get; set; }

        public string SelectedFilesId { get; set; }
    }

    public enum viCompo_ModalFileListSort
    {
        TitleAes = 0,
        TitleDes = 1,
        FileTypeAes = 2,
        FileTypeDes = 3
    }
}
