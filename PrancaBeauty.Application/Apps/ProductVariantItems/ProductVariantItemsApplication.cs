using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductVariantItemsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariantItems
{
    public class ProductVariantItemsApplication : IProductVariantItemsApplication
    {
        private readonly ILogger _Logger;
        private readonly IProductVariantItemsRepository _ProductVariantItemsRepository;

        public ProductVariantItemsApplication(IProductVariantItemsRepository productVariantItemsRepository, ILogger logger)
        {
            _ProductVariantItemsRepository = productVariantItemsRepository;
            _Logger = logger;
        }

       
    }
}
