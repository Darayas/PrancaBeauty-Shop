using Framework.Common.Utilities.Paging;
using Framework.Infrastructure;
using PrancaBeauty.Application.Apps.Categories;
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
        private readonly ICategoryApplication _CategoryApplication;
        public ProductApplication(IProductRepository productRepository, ILogger logger, ICategoryApplication categoryApplication)
        {
            _ProductRepository = productRepository;
            _Logger = logger;
            _CategoryApplication = categoryApplication;
        }

        public async Task<(OutPagingData, List<OutGetProductsForManage>)> GetProductsForManageAsync(string LangId, string SellerUserId, string Title, string Name, bool? IsConfirmed, bool? IsDraft, bool? IsDelete)
        {
            try
            {
                var qData = _ProductRepository.Get
                                              .Where(a => SellerUserId != null ? a.tblProductSellers.Where(b => b.SellerUserId == Guid.Parse(SellerUserId)).Any() : true)
                                              .Select(a => new OutGetProductsForManage()
                                              {
                                                  Id = a.Id.ToString(),
                                                  Name = a.Name,
                                                  Title = a.Title,
                                                  Date = a.Date,
                                                  CountAsks = a.tblProductAsk.Count(),
                                                  CountReviews = a.tblProductReviews.Count(),
                                                  CountSellers = a.tblProductSellers.Count(),
                                                  CountVisit = 0,
                                                  Status = a.IsDelete ? OutGetProductsForManage_Status.IsDelete : (a.IsDraft ? OutGetProductsForManage_Status.IsDraft : (a.IsConfirmed ? OutGetProductsForManage_Status.IsConfirm : (a.Date > DateTime.Now ? OutGetProductsForManage_Status.IsSchedule : OutGetProductsForManage_Status.UnKnown))),
                                                  AuthorUserId = a.AuthorUserId.ToString(),
                                                  AuthorName = a.tblAuthorUser.FirstName + " " + a.tblAuthorUser.LastName,
                                                  AuthorImageUrl = "",
                                                  AuthorUserName = a.tblAuthorUser.UserName,
                                                  CategoryName = a.tblCategory.Name,
                                                  CategoryTitle = a.tblCategory.tblCategory_Translates.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single(),
                                                  CategoryMapTitle=a.tblCategory.tblCategory_Parent.tblCategory_Parent.tblCategory_Parent
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
