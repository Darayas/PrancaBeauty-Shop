using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.FilePath
{
    public class InpCheckDirectoryExist
    {
        [Display(Name = "FileServerId")]
        [RequiredString]
        [GUID]
        public string FileServerId { get; set; }

        [Display(Name = "Path")]
        [RequiredString]
        [MaxLengthString(300)]
        public string Path { get; set; }
    }
}
