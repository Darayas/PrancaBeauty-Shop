using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCategoriesList
    {
        public string Id { get; set; }

        [Display(Name = "TopicTitle")]
        public string TopicTitle { get; set; }

        public string ParentId { get; set; }

        [Display(Name = "ImgUrl")]
        public string ImgUrl { get; set; }

        [Display(Name = "ParentTitle")]
        public string ParentTitle { get; set; }

        [Display(Name = "Name")]

        public string Name { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
        public int Sort { get; set; }
    }
}
