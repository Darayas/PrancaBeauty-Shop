using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Files
{
    public class InpAddFile
    {
        [Display(Name ="Id")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "FileServerId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string FileServerId { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100,ErrorMessage = "MaxLengthMsg")]
        public string Title { get; set; }

        [Display(Name = "Path")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(300, ErrorMessage = "MaxLengthMsg")]
        public string Path { get; set; }

        [Display(Name = "FileName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string FileName { get; set; }

        [Display(Name = "SizeOnDisk")]
        public long SizeOnDisk { get; set; }

        [Display(Name = "MimeType")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string MimeType { get; set; } // image/jpg, image/png , application/zip

        [Display(Name = "IsPrivate")]
        public bool IsPrivate { get; set; }
    }
}
