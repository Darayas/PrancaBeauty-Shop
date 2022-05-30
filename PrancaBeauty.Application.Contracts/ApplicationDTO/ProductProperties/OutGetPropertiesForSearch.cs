using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductProperties
{
    public class OutGetPropertiesForSearch
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public List<OutGetPropertiesForSearchItem> ValueItems { get; set; }
    }

    public class OutGetPropertiesForSearchItem
    {
        public string Title { get; set; }
    }
}
