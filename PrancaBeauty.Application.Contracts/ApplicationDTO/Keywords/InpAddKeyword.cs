using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Keywords
{
    public class InpAddKeyword
    {
        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [MaxLengthString(5000)]
        public string Description { get; set; }
    }
}
