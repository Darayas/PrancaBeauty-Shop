using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompo_PropertisSearch
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public List<vmCompo_PropertisSearchItem> ValueItems { get; set; }
    }

    public class vmCompo_PropertisSearchItem
    {
        public string Title { get; set; }
    }
}
