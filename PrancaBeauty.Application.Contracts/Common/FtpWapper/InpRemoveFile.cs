using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Common.FtpWapper
{
    public class InpRemoveFile
    {
        [Display(Name = "FileId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string FileId { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }
    }
}
