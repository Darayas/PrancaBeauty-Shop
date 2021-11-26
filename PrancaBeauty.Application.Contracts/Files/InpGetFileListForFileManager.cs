using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Files
{
    public class InpGetFileListForFileManager
    {
        [Display(Name = "FileTypeId")]
        [GUID]
        public string FileTypeId { get; set; }

        [Display(Name = "UploaderUserId")]
        [GUID]
        public string UploaderUserId { get; set; }

        [Display(Name = "Sort")]
        public InpGetFileListForFileManagerSort Sort { get; set; }

        [Display(Name = "FileTitle")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string FileTitle { get; set; }

        [Display(Name = "Take")]
        public int Take { get; set; }

        [Display(Name = "CurrentPage")]
        public int CurrentPage { get; set; }
    }

    public enum InpGetFileListForFileManagerSort
    {
        DateDes = 0,
        DateAes = 1,
        TitleAes = 2,
        TitleDes = 3,
        FileTypeAes = 4,
        FileTypeDes = 5
    }

}
