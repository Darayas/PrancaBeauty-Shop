using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
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

        public async Task<(OutPagingData, List<OutGetProductsForManage>)> GetProductsForManageAsync(int Page, int Take, string LangId, string SellerUserId, string AuthorUserId, string Title, string Name, bool? IsDelete, bool? IsDraft, bool? IsConfirmed, bool? IsSchedule)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LangId))
                    throw new ArgumentInvalidException($"'{nameof(LangId)}' cannot be null or whitespace.");

                var qData = _ProductRepository.Get
                                              .Where(a => SellerUserId != null ? a.tblProductSellers.Where(b => b.SellerUserId == Guid.Parse(SellerUserId)).Any() : true)
                                              .Where(a => a.LangId == Guid.Parse(LangId))
                                              .Select(a => new OutGetProductsForManage
                                              {
                                                  Id = a.Id.ToString(),
                                                  ImgUrl = a.tblProductMedia.Where(b => b.tblFiles.MimeType.StartsWith("image"))
                                                                            .Select(b => b.tblFiles.tblFileServer.HttpDomin
                                                                                         + b.tblFiles.tblFileServer.HttpPath
                                                                                         + b.tblFiles.Path
                                                                                         + b.tblFiles.FileName).First(),
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
                                                  CategoryId = a.CategoryId.ToString(),
                                                  IsDelete = a.IsDelete,
                                                  IsConfirm = a.IsConfirmed,
                                                  IsDraft = a.IsDraft
                                              });

                #region جستوجو
                {
                    if (AuthorUserId != null)
                        qData = qData.Where(a => a.AuthorUserId == AuthorUserId);

                    if (Title != null)
                        qData = qData.Where(a => a.Title.Contains(Title));

                    if (Name != null)
                        qData = qData.Where(a => a.Name.Contains(Name));

                    if (IsDelete != null)
                        qData = qData.Where(a => IsDelete.Value ? a.IsDelete == true : a.IsDelete == false);

                    if (IsDraft != null)
                        qData = qData.Where(a => IsDraft.Value ? a.IsDraft == true : a.IsDraft == false);

                    if (IsConfirmed != null)
                        qData = qData.Where(a => IsConfirmed.Value ? a.IsConfirm == true : a.IsConfirm == false);

                    if (IsSchedule != null)
                        qData = qData.Where(a => IsSchedule.Value ? a.Status == OutGetProductsForManage_Status.IsSchedule : a.Status != OutGetProductsForManage_Status.IsSchedule);
                }
                #endregion

                #region مرتب سازی

                #endregion

                #region صفحه بندی
                var _PagingData = PagingData.Calc(await qData.LongCountAsync(), Page, Take);
                #endregion

                return (_PagingData,
                                await qData.OrderByDescending(a => a.Date)
                                           .Skip((int)_PagingData.Skip)
                                           .Take((int)_PagingData.Take)
                                           .ToListAsync());
            }
            catch (ArgumentInvalidException)
            {
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }
    }
}
