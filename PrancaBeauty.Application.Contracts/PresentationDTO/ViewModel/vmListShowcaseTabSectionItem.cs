using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmListShowcaseTabSectionItem
    {
        public string Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Sort")]
        public int Sort { get; set; }

        [Display(Name = "SectionType")]
        public string SectionType { get; set; }
    }

    
}
