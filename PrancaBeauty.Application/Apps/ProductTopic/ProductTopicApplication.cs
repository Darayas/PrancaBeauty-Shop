using PrancaBeauty.Domin.Product.ProductTopicAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductTopic
{
    public class ProductTopicApplication : IProductTopicApplication
    {
        private readonly IProductTopicRepository _ProductTopicRepository;

        public ProductTopicApplication(IProductTopicRepository productTopicRepository)
        {
            _ProductTopicRepository = productTopicRepository;
        }
    }
}
