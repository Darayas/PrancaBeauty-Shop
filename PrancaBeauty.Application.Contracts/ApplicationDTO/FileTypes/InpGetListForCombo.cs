using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.FileTypes
{
    public class InpGetListForCombo
    {
        [Display(Name = "Title")]
        [MaxLengthString(100)]
        public string Title { get; set; }
    }
}
