using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompo_VariantSearch
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<vmCompo_VariantSearchItem> ValueItems { get; set; }
    }

    public class vmCompo_VariantSearchItem
    {
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
