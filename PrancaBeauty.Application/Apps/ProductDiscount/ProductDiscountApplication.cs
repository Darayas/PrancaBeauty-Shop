using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductDiscountAgg.Contracts;

namespace PrancaBeauty.Application.Apps.ProductDiscount
{
    public class ProductDiscountApplication : IProductDiscountApplication
    {
        private readonly ILogger _Logger;
        private readonly IProductDiscountRepository _ProductDiscountRepository;
        public ProductDiscountApplication(ILogger logger, IProductDiscountRepository productDiscountRepository)
        {
            _Logger=logger;
            _ProductDiscountRepository=productDiscountRepository;
        }

    }
}
