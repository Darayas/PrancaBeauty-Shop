using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viEditSlide
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "FileId")]
        [RequiredString]
        [GUID]
        public string FileId { get; set; }

        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(200)]
        public string Title { get; set; }

        [Display(Name = "Url")]
        [RequiredString]
        [MaxLengthString(500)]
        [ItsForUrl]
        public string Url { get; set; }

        [Display(Name = "IsFollow")]
        public bool IsFollow { get; set; } // Follow, NoFollow

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        [Display(Name = "StartDate")]
        // TODO DA: DateTime
        public string StartDate { get; set; }

        [Display(Name = "EndDate")]
        // TODO DA: DateTime
        public string EndDate { get; set; }
    }
}
