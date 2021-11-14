using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.Application.Apps.ProductPropertiesValues;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
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
        private readonly ILocalizer _Localizer;
        private readonly IProductRepository _ProductRepository;
        private readonly ICategoryApplication _CategoryApplication;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;
        private readonly IProductPropertiesValuesApplication _ProductPropertiesValuesApplication;
        public ProductApplication(IProductRepository productRepository, ILogger logger, ICategoryApplication categoryApplication, ILocalizer localizer, IProductVariantItemsApplication productVariantItemsApplication, IProductPropertiesValuesApplication productPropertiesValuesApplication)
        {
            _ProductRepository = productRepository;
            _Logger = logger;
            _CategoryApplication = categoryApplication;
            _Localizer = localizer;
            _ProductVariantItemsApplication = productVariantItemsApplication;
            _ProductPropertiesValuesApplication = productPropertiesValuesApplication;
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
                                                  ImgUrl = a.tblProductMedia.Where(b => b.tblFiles.tblFileTypes.MimeType.StartsWith("image"))
                                                                            .Select(b => b.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                                         + b.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                                         + b.tblFiles.tblFilePaths.Path
                                                                                         + b.tblFiles.FileName).First(),
                                                  UniqueNumber = a.UniqueNumber,
                                                  Name = a.Name,
                                                  Title = a.Title,
                                                  Date = a.Date,
                                                  HasUnConfirmedAsk = a.tblProductAsk.Any(b => b.IsConfirm == false),
                                                  CountAsks = a.tblProductAsk.Count(),
                                                  HasUnConfirmedReviews = a.tblProductReviews.Any(b => b.IsConfirm == false),
                                                  CountReviews = a.tblProductReviews.Count(),
                                                  HasUnConfirmedSellerRequest = a.tblProductSellers.Any(b => b.IsConfirm == false),
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

        public async Task<OperationResult> AddProdcutAsync(InpAddProdcut Input, string AuthorUserId)
        {
            try
            {
                #region Validations
                if (Input is null)
                    throw new ArgumentInvalidException(nameof(Input));

                if (Input.IsDraft)
                {
                    if (string.IsNullOrWhiteSpace(Input.LangId))
                        throw new ArgumentInvalidException($"{nameof(Input.LangId)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.Title))
                        throw new ArgumentInvalidException($"{nameof(Input.Title)} cant be null or whitespace.");

                    if (!Input.Name.CheckCharsForProductTitle())
                        throw new ArgumentInvalidException(_Localizer["ItsForProductTitleMsg"]);

                    if (string.IsNullOrWhiteSpace(AuthorUserId))
                        throw new ArgumentInvalidException($"{nameof(AuthorUserId)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.Name) == false)
                        if (!Input.Name.CheckCharsForUrlName())
                            throw new ArgumentInvalidException(_Localizer["ItsForUrlMsg"]);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(AuthorUserId))
                        throw new ArgumentInvalidException($"{nameof(AuthorUserId)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.LangId))
                        throw new ArgumentInvalidException($"{nameof(Input.LangId)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.TopicId))
                        throw new ArgumentInvalidException($"{nameof(Input.TopicId)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.CategoryId))
                        throw new ArgumentInvalidException($"{nameof(Input.CategoryId)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.Name))
                        throw new ArgumentInvalidException($"{nameof(Input.Name)} cant be null or whitespace.");

                    if (!Input.Name.CheckCharsForUrlName())
                        throw new ArgumentInvalidException(_Localizer["ItsForUrlMsg"]);

                    if (string.IsNullOrWhiteSpace(Input.Title))
                        throw new ArgumentInvalidException($"{nameof(Input.Title)} cant be null or whitespace.");

                    if (!Input.Name.CheckCharsForProductTitle())
                        throw new ArgumentInvalidException(_Localizer["ItsForProductTitleMsg"]);

                    if (string.IsNullOrWhiteSpace(Input.Date))
                        throw new ArgumentInvalidException($"{nameof(Input.Date)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.Price))
                        throw new ArgumentInvalidException($"{nameof(Input.Price)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.Price))
                        throw new ArgumentInvalidException($"{nameof(Input.MetaTagKeyword)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.MetaTagCanonical))
                        throw new ArgumentInvalidException($"{nameof(Input.MetaTagCanonical)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.MetaTagDescreption))
                        throw new ArgumentInvalidException($"{nameof(Input.MetaTagDescreption)} cant be null or whitespace.");

                    if (string.IsNullOrWhiteSpace(Input.Description))
                        throw new ArgumentInvalidException($"{nameof(Input.Description)} cant be null or whitespace.");

                    if (Input.Properties is null)
                        throw new ArgumentInvalidException($"{nameof(Input.Properties)} cant be null.");

                    if (Input.Properties.Any(a => string.IsNullOrWhiteSpace(a.Value)))
                        throw new ArgumentInvalidException($"{nameof(Input.Properties)} value cant be null or whitespace.");

                    if (Input.Keywords is null)
                        throw new ArgumentInvalidException($"{nameof(Input.Keywords)} cant be null.");

                    if (Input.Keywords.Count() == 0)
                        throw new ArgumentInvalidException($"keyword count must be greater than zero.");


                }

                #endregion

                // برسی تکراری نبودن نام محصول
                if (await CheckDuplicateNameAsync(Input.Name.ToNormalizedUrl()))
                    return new OperationResult().Failed("ProdcutName is duplicate.");

                string ProductId = new Guid().SequentialGuid().ToString();

                #region افزودن محصول به جدول اصلی
                {
                    var tProduct = new tblProducts()
                    {
                        Id = Guid.Parse(ProductId),
                        AuthorUserId = Guid.Parse(AuthorUserId),
                        CategoryId = Input.CategoryId != null ? Guid.Parse(Input.CategoryId) : null,
                        TopicId = Input.TopicId != null ? Guid.Parse(Input.TopicId) : null,
                        LangId = Guid.Parse(Input.LangId),
                        UniqueNumber = await GenerateUniqeNumberAsync(),
                        Name = Input.Name.ToNormalizedUrl(),
                        Title = Input.Title,
                        Date = Input.Date == null ? DateTime.Now : (Convert.ToDateTime(Input.Date).AddHours(1) < DateTime.Now ? DateTime.Now : Convert.ToDateTime(Input.Date)),
                        IsConfirmed = false,
                        IsDelete = false,
                        IsDraft = Input.IsDraft,
                        MetaTagCanonical = Input.MetaTagCanonical,
                        MetaTagDescreption = Input.MetaTagDescreption,
                        MetaTagKeyword = Input.MetaTagKeyword,
                        Description = Input.Description.GetSanitizeHtml()
                    };

                    await _ProductRepository.AddAsync(tProduct, default, true);
                }
                #endregion

                #region خصوصیات محصول
                {
                    var _Result = await _ProductPropertiesValuesApplication.AddPropertiesToProductAsync(ProductId, Input.Properties.Select(a => new KeyValuePair<string, string>(a.Id, a.Value)).ToDictionary(a => a.Key, a => a.Value));
                    if (_Result.IsSucceeded == false)
                    {
                        // حذف محصول
                        await _ProductRepository.DeleteAsync(Guid.Parse(ProductId), default, true);

                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region کلمات کلیدی
                // TODO کلمات کلیدی
                #endregion

                #region تنوع محصول
                // TODO تنوع محصول
                #endregion

                #region محدودیت های ارسال پستی
                // TODO محدودیت های ارسال پستی
                #endregion

                return default;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        private async Task<bool> CheckDuplicateNameAsync(string Name, string ProductId = null)
        {
            var qResult = await _ProductRepository.Get
                                                  .Where(a => a.Name == Name)
                                                  .Where(a => ProductId != null ? a.Id != Guid.Parse(ProductId) : true)
                                                  .AnyAsync();

            return qResult;

        }

        private async Task<string> GenerateUniqeNumberAsync()
        {
            string RndNum = null;
            bool qResult = false;
            do
            {
                RndNum = new Random().Next(1000, 9999).ToString() + new Random().Next(100, 999).ToString();
                qResult = await _ProductRepository.Get.AnyAsync(a => a.UniqueNumber == RndNum);
                //if (qResult == false)
                //    break;

            } while (qResult == true);

            return RndNum;
        }
    }
}
