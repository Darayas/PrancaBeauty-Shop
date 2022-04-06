using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
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
        DateDes = 0,
        DateAes = 1,
        TitleAes = 2,
        TitleDes = 3,
        FileTypeAes = 4,
        FileTypeDes = 5
    }
}
