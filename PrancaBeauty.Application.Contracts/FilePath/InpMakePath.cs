using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.FilePath
{
   public class InpMakePath
    {
        [Display(Name = "FileServerId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string FileServerId { get; set; }

        [Display(Name = "Path")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(300, ErrorMessage = "MaxLength")]
        public string Path { get; set; }
    }
}
