using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductVariantItems
{
    public class OutGetAllProductVariantsForProductDetails
    {
        public string Title { get; set; }
        public string VariantType { get; set; }

        public List<OutGetAllProductVariantsForProductDetailsItem> LstItems { get; set; }
    }

    public class OutGetAllProductVariantsForProductDetailsItem
    {
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
