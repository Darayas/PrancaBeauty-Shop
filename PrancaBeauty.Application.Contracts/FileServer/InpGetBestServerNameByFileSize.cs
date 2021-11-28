using Framework.Common.DataAnnotations.Numbers.All;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.FileServer
{
    public class InpGetBestServerNameByFileSize
    {
        [Display(Name = "FileSize")]
        [NumRange(1, long.MaxValue)]
        public long FileSize { get; set; }
    }
}
