using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.FileServer
{
    public class InpGetBestServerNameByFileSize
    {
        [Display(Name = "FileSize")]
        [Range(1, long.MaxValue, ErrorMessage = "RangeMsg")]
        public long FileSize { get; set; }
    }
}
