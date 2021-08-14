using PrancaBeauty.Domin.Product.ProductPropertisAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPropertis
{
    public class ProductPropertisApplication : IProductPropertisApplication
    {
        private readonly IProductPropertisRepository _ProductPropertisRepository;

        public ProductPropertisApplication(IProductPropertisRepository productPropertisRepository)
        {
            _ProductPropertisRepository = productPropertisRepository;
        }
    }
}
