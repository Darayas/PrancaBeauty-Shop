using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductAskLikes
{
    public class ProductAskLikesApplication : IProductAskLikesApplication
    {
        private readonly IProductAskLikesRepository _ProductAskLikesRepository;

        public ProductAskLikesApplication(IProductAskLikesRepository productAskLikesRepository)
        {
            _ProductAskLikesRepository = productAskLikesRepository;
        }
    }

}
