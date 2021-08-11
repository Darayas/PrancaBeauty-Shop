using PrancaBeauty.Domin.Product.ProductAskAgg.Contarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductAsk
{
    public class ProductAskApplication : IProductAskApplication
    {
        private readonly IProductAskRepository _ProductAskRepository;

        public ProductAskApplication(IProductAskRepository productAskRepository)
        {
            _ProductAskRepository = productAskRepository;
        }
    }
}
