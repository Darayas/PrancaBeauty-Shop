using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.SearchHistory
{
    public class OutGetDataForAutoComplete
    {
        public List<OutGetDataForAutoComplete_Products> LstProducts { get; set; }
        public List<OutGetDataForAutoComplete_RelatedCategories> LstRelatedCategory { get; set; }
        public List<OutGetDataForAutoComplete_RelatedKeywords> LstRelatedKeywords { get; set; }
        public List<OutGetDataForAutoComplete_RelatedWords> LstRelatedWords { get; set; }
    }

    public class OutGetDataForAutoComplete_Products
    {
        public string Id { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class OutGetDataForAutoComplete_RelatedCategories
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string ParentTitle { get; set; }
    }

    public class OutGetDataForAutoComplete_RelatedKeywords
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class OutGetDataForAutoComplete_RelatedWords
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
