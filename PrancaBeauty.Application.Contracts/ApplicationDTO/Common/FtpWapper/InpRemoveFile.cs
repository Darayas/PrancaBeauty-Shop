using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Common.FtpWapper
{
    public class InpRemoveFile
    {
        [Display(Name = "FileId")]
        [RequiredString]
        [GUID]
        public string FileId { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }
    }
}
