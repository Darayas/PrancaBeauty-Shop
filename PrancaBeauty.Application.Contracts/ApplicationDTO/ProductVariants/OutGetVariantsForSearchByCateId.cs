using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariants
{
    public class OutGetVariantsForSearchByCateId
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<OutGetVariantsForSearchByCateIdItem> ValueItems { get; set; }
    }

    public class OutGetVariantsForSearchByCateIdItem
    {
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
