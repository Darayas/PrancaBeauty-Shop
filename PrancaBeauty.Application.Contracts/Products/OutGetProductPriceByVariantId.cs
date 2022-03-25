using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Products
{
    public class OutGetProductPriceByVariantId
    {
        public double ProductPrice { get; set; }
        public double ProductOldPrice { get; set; }
        public string CurrencySymbol { get; set; }
    }
}
