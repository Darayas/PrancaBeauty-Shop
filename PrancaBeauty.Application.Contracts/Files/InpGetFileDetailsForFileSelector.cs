using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Files
{
    public class InpGetFileDetailsForFileSelector
    {
        [Display(Name = "FileId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string FileId { get; set; }
    }
}
