using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Categories
{
    public class InpGetCategoriesForSeachAutoComplete
    {
        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Title { get; set; }
    }
}
