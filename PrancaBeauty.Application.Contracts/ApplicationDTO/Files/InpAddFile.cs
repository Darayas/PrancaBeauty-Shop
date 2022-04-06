using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Files
{
    public class InpAddFile
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "FileServerId")]
        [RequiredString]
        [GUID]
        public string FileServerId { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "Path")]
        [RequiredString]
        [MaxLengthString(300)]
        public string Path { get; set; }

        [Display(Name = "FileName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string FileName { get; set; }

        [Display(Name = "SizeOnDisk")]
        public long SizeOnDisk { get; set; }

        [Display(Name = "MimeType")]
        [RequiredString]
        [MaxLengthString(100)]
        public string MimeType { get; set; } // image/jpg, image/png , application/zip

        [Display(Name = "IsPrivate")]
        public bool IsPrivate { get; set; }
    }
}
