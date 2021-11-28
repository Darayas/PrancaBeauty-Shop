using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Files
{
    public class InpGetFileDetailsForFileSelector
    {
        [Display(Name = "FileId")]
        [RequiredString]
        [GUID]
        public string FileId { get; set; }
    }
}
