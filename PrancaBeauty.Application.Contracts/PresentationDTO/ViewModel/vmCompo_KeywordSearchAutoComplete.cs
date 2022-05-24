using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompo_KeywordSearchAutoComplete
    {
        public List<vmCompo_KeywordSearchAutoComplete_Products> LstProducts { get; set; }
        public List<vmCompo_KeywordSearchAutoComplete_RelatedCategories> LstRelatedCategory { get; set; }
        public List<vmCompo_KeywordSearchAutoComplete_RelatedKeywords> LstRelatedKeywords { get; set; }
        public List<vmCompo_KeywordSearchAutoComplete_RelatedWords> LstRelatedWords { get; set; }
    }

    public class vmCompo_KeywordSearchAutoComplete_Products
    {
        public string Id { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class vmCompo_KeywordSearchAutoComplete_RelatedCategories
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string ParentTitle { get; set; }
    }

    public class vmCompo_KeywordSearchAutoComplete_RelatedKeywords
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class vmCompo_KeywordSearchAutoComplete_RelatedWords
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
