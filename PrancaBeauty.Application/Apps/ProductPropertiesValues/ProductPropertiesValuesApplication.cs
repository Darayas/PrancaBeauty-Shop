using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPropertiesValues
{
    public class ProductPropertiesValuesApplication : IProductPropertiesValuesApplication
    {
        private readonly IProductPropertiesValuesRepository _ProductPropertiesValuesRepository;

        public ProductPropertiesValuesApplication(IProductPropertiesValuesRepository productPropertiesValuesRepository)
        {
            _ProductPropertiesValuesRepository = productPropertiesValuesRepository;
        }
    }
}
