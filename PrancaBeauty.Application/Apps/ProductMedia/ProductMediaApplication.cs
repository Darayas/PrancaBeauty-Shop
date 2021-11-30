using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.ProductMedia;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductMediaAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductMedia
{
    public class ProductMediaApplication : IProductMediaApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductMediaRepository _ProductMediaRepository;

        public ProductMediaApplication(ILogger logger, IProductMediaRepository productMediaRepository, IServiceProvider serviceProvider)
        {
            _Logger = logger;
            _ProductMediaRepository = productMediaRepository;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddMediasToProductsAsync(InpAddMediasToProduct Input)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
