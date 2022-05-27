using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.SearchHistory
{
    public class OutGetWordDataForAutoComplete
    {
        public List<OutGetDataForAutoComplete_Products> LstProducts { get; set; } = new List<OutGetDataForAutoComplete_Products>();
        public List<OutGetDataForAutoComplete_RelatedCategories> LstRelatedCategory { get; set; } = new List<OutGetDataForAutoComplete_RelatedCategories>();
        public List<OutGetDataForAutoComplete_RelatedKeywords> LstRelatedKeywords { get; set; } = new List<OutGetDataForAutoComplete_RelatedKeywords>();
        public List<OutGetDataForAutoComplete_RelatedWords> LstRelatedWords { get; set; } = new List<OutGetDataForAutoComplete_RelatedWords>();
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
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class OutGetDataForAutoComplete_RelatedWords
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
