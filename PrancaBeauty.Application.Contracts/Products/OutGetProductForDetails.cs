using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Products
{
    public class OutGetProductForDetails
    {
        public string Id { get; set; }
        public string TopicId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public double AvgStarRating { get; set; }
        public int CountUserInStarRating { get; set; }
        public int CountReviews { get; set; }
        public int CountAsk { get; set; }
        public double Price { get; set; } // TODO Discount
        public string CurrencySymbol { get; set; }
        public string MetaDescription { get; set; }
        public string MetaCanonical { get; set; }
        public string MetaKeyword { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public List<OutGetProductForDetails_Media> LstMedia { get; set; }
        public List<OutGetProductForDetails_Properties> LstProperties { get; set; }
    }

    public class OutGetProductForDetails_Media
    {
        public string MediaUrl { get; set; }
        public string MediaTitle { get; set; }
        public string MimeType { get; set; }
        public int Sort { get; set; }
    }

    public class OutGetProductForDetails_Properties
    {
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
