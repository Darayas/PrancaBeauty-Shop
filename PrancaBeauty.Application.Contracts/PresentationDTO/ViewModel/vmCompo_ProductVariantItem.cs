using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompo_ProductVariantItem
    {
        public string Title { get; set; }
        public string VariantType { get; set; }
        public string DefaultVariantValue { get; set; }    

        public List<vmLstCompo_ProductVariantItem> LstItems { get; set; }
    }

    public class vmLstCompo_ProductVariantItem
    {
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
