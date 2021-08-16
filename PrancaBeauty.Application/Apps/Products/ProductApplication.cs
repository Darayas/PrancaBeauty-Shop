using Framework.Common.Utilities.Paging;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.Domin.Product.ProductAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Products
{
    public class ProductApplication : IProductApplication
    {
        private readonly ILogger _Logger;
        private readonly IProductRepository _ProductRepository;
        public ProductApplication(IProductRepository productRepository, ILogger logger)
        {
            _ProductRepository = productRepository;
            _Logger = logger;
        }

        public async Task<(OutPagingData, List<OutGetProductsForManage>)> GetProductsForManageAsync(string SellerUserId, string Title, string Name, bool? IsConfirmed, bool? IsDraft, bool? IsDelete)
        {
            try
            {
                var qData = _ProductRepository.Get
                                              .Where(a => SellerUserId != null ? a.tblProductSellers.Where(b => b.SellerUserId == Guid.Parse(SellerUserId)).Any() : true)
                                              .Select(a => new OutGetProductsForManage()
                                              {
                                                  
                                              });
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                throw;
            }
        }
    }
}
