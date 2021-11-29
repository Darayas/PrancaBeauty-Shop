using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.Application.Apps.Currency;
using PrancaBeauty.Application.Apps.KeywordsProducts;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.PostingRestrictions;
using PrancaBeauty.Application.Apps.ProductPrices;
using PrancaBeauty.Application.Apps.ProductPropertiesValues;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Currency;
using PrancaBeauty.Application.Contracts.KeywordProducts;
using PrancaBeauty.Application.Contracts.Languages;
using PrancaBeauty.Application.Contracts.PostingRestrictions;
using PrancaBeauty.Application.Contracts.ProductPrice;
using PrancaBeauty.Application.Contracts.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Products
{
    public class ProductApplication : IProductApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IMapper _Mapper;
        private readonly IProductPriceApplication _ProductPriceApplication;
        private readonly IProductRepository _ProductRepository;
        private readonly ICategoryApplication _CategoryApplication;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;
        private readonly IProductPropertiesValuesApplication _ProductPropertiesValuesApplication;
        private readonly IKeywordProductsApplication _KeywordProductsApplication;
        private readonly IPostingRestrictionsApplication _PostingRestrictionsApplication;
        private readonly ICurrencyApplication _CurrencyApplication;
        private readonly ILanguageApplication _LanguageApplication;
        public ProductApplication(IProductRepository productRepository, ILogger logger, ICategoryApplication categoryApplication, ILocalizer localizer, IProductVariantItemsApplication productVariantItemsApplication, IProductPropertiesValuesApplication productPropertiesValuesApplication, IKeywordProductsApplication keywordProductsApplication, IMapper mapper, IPostingRestrictionsApplication postingRestrictionsApplication, IProductPriceApplication productPriceApplication = null, ICurrencyApplication currencyApplication = null, ILanguageApplication languageApplication = null, IServiceProvider serviceProvider = null)
        {
            _ProductRepository = productRepository;
            _Logger = logger;
            _CategoryApplication = categoryApplication;
            _Localizer = localizer;
            _ProductVariantItemsApplication = productVariantItemsApplication;
            _ProductPropertiesValuesApplication = productPropertiesValuesApplication;
            _KeywordProductsApplication = keywordProductsApplication;
            _Mapper = mapper;
            _PostingRestrictionsApplication = postingRestrictionsApplication;
            _ProductPriceApplication = productPriceApplication;
            _CurrencyApplication = currencyApplication;
            _LanguageApplication = languageApplication;
            _ServiceProvider = serviceProvider;
        }

        public async Task<(OutPagingData, List<OutGetProductsForManage>)> GetProductsForManageAsync(InpGetProductsForManage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _ProductRepository.Get
                                              .Where(a => Input.SellerUserId != null ? a.tblProductSellers.Where(b => b.SellerUserId == Guid.Parse(Input.SellerUserId)).Any() : true)
                                              .Where(a => a.LangId == Guid.Parse(Input.LangId))
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
                                                  CategoryTitle = a.tblCategory.tblCategory_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                                                  CategoryId = a.CategoryId.ToString(),
                                                  IsDelete = a.IsDelete,
                                                  IsConfirm = a.IsConfirmed,
                                                  IsDraft = a.IsDraft
                                              });

                #region جستوجو
                {
                    if (Input.AuthorUserId != null)
                        qData = qData.Where(a => a.AuthorUserId == Input.AuthorUserId);

                    if (Input.Title != null)
                        qData = qData.Where(a => a.Title.Contains(Input.Title));

                    if (Input.Name != null)
                        qData = qData.Where(a => a.Name.Contains(Input.Name));

                    if (Input.IsDelete != null)
                        qData = qData.Where(a => Input.IsDelete.Value ? a.IsDelete == true : a.IsDelete == false);

                    if (Input.IsDraft != null)
                        qData = qData.Where(a => Input.IsDraft.Value ? a.IsDraft == true : a.IsDraft == false);

                    if (Input.IsConfirmed != null)
                        qData = qData.Where(a => Input.IsConfirmed.Value ? a.IsConfirm == true : a.IsConfirm == false);

                    if (Input.IsSchedule != null)
                        qData = qData.Where(a => Input.IsSchedule.Value ? a.Status == OutGetProductsForManage_Status.IsSchedule : a.Status != OutGetProductsForManage_Status.IsSchedule);
                }
                #endregion

                #region مرتب سازی

                #endregion

                #region صفحه بندی
                var _PagingData = PagingData.Calc(await qData.LongCountAsync(), Input.Page, Input.Take);
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

        public async Task<OperationResult> AddProdcutAsync(InpAddProdcut Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                if (Input.Properties is null)
                    throw new ArgumentInvalidException($"Properties cant be null.");

                if (Input.Properties.Any(a => string.IsNullOrWhiteSpace(a.Value)))
                    throw new ArgumentInvalidException($"Properties value cant be null or whitespace.");

                if (Input.Keywords is null)
                    throw new ArgumentInvalidException($"Keywords cant be null.");

                if (Input.Keywords.Count() == 0)
                    throw new ArgumentInvalidException($"keyword count must be greater than zero.");
                #endregion

                // برسی تکراری نبودن نام محصول
                if (await CheckDuplicateNameAsync(Input.Name.ToLowerCaseUrl()))
                    return new OperationResult().Failed("ProdcutName is duplicate.");

                string ProductId = new Guid().SequentialGuid().ToString();

                #region افزودن محصول به جدول اصلی
                {
                    var tProduct = new tblProducts()
                    {
                        Id = Guid.Parse(ProductId),
                        AuthorUserId = Guid.Parse(Input.AuthorUserId),
                        CategoryId = Input.CategoryId != null ? Guid.Parse(Input.CategoryId) : null,
                        TopicId = Input.TopicId != null ? Guid.Parse(Input.TopicId) : null,
                        LangId = Guid.Parse(Input.LangId),
                        UniqueNumber = await GenerateUniqeNumberAsync(),
                        Name = Input.Name.ToLowerCaseUrl(),
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
                    var _Result = await _ProductPropertiesValuesApplication.AddPropertiesToProductAsync(new InpAddPropertiesToProduct
                    {
                        ProductId = ProductId,
                        PropItems = Input.Properties.Select(a => new InpAddPropertiesToProduct_Items { Id = a.Id, Value = a.Value }).ToList()
                    });

                    if (_Result.IsSucceeded == false)
                    {
                        // حذف محصول
                        await _ProductRepository.DeleteAsync(Guid.Parse(ProductId), default, true);

                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region کلمات کلیدی
                {
                    var _Result = await _KeywordProductsApplication.AddKeywordsToProductAsync(new InpAddKeywordsToProduct() { ProductId = ProductId, LstKeywords = Input.Keywords.Select(a => new InpAddKeywordsToProduct_LstKeywords() { Title = a.Title, Similarity = a.Similarity }).ToList() });
                    if (!_Result.IsSucceeded)
                    {
                        // حذف خصوصیات
                        await _ProductPropertiesValuesApplication.RemovePropertiesByProductIdAsync(new InpRemovePropertiesByProductId() { ProductId = ProductId });

                        // حذف محصول
                        await _ProductRepository.DeleteAsync(Guid.Parse(ProductId), default, true);

                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region تنوع محصول
                {
                    var _VarinatData = _Mapper.Map<InpAddVariantsToProduct>(Input);
                    _VarinatData.ProductId = ProductId;
                    _VarinatData.SellerId = Input.AuthorUserId;

                    var _Result = await _ProductVariantItemsApplication.AddVariantsToProductAsync(_VarinatData);
                    if (!_Result.IsSucceeded)
                    {
                        // حذف کلمات کلیدی
                        await _KeywordProductsApplication.RemoveAllProductKeywordsAsync(new InpRemoveAllProductKeywords() { ProductId = ProductId });

                        // حذف خصوصیات
                        await _ProductPropertiesValuesApplication.RemovePropertiesByProductIdAsync(new InpRemovePropertiesByProductId() { ProductId = ProductId });

                        // حذف محصول
                        await _ProductRepository.DeleteAsync(Guid.Parse(ProductId), default, true);


                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region محدودیت های ارسال پستی
                {
                    var _PostingRestrictionsData = _Mapper.Map<InpAddPostingRestrictionsToProduct>(Input.PostingRestrictions);
                    _PostingRestrictionsData.ProductId = ProductId;

                    var _Result = await _PostingRestrictionsApplication.AddPostingRestrictionsToProductAsync(_PostingRestrictionsData);
                    if (_Result.IsSucceeded == false)
                    {
                        // حذف تنوع محصول
                        await _ProductVariantItemsApplication.RemoveAllVariantsFromProductAsync(new InpRemoveVariantsFromProduct() { ProductId = ProductId });

                        // حذف کلمات کلیدی
                        await _KeywordProductsApplication.RemoveAllProductKeywordsAsync(new InpRemoveAllProductKeywords() { ProductId = ProductId });

                        // حذف خصوصیات
                        await _ProductPropertiesValuesApplication.RemovePropertiesByProductIdAsync(new InpRemovePropertiesByProductId() { ProductId = ProductId });

                        // حذف محصول
                        await _ProductRepository.DeleteAsync(Guid.Parse(ProductId), default, true);

                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region ثبت قیمت محصول
                {
                    var _Result = await _ProductPriceApplication.AddPriceToProductAsyc(new InpAddPriceToProduct
                    {
                        ProductId = ProductId,
                        UserId = Input.AuthorUserId,
                        Price = Input.Price,
                        CurrencyId = await _CurrencyApplication.GetIdByCountryIdAsync(new InpGetIdByCountryId
                        {
                            CountryId = await _LanguageApplication.GetCountryIdByLangIdAsync(new InpGetCountryIdByLangId
                            {
                                LangId = Input.LangId
                            })
                        })
                    });
                    if (_Result.IsSucceeded == false)
                    {
                        // حذف محدودیت های ارسال

                        // حذف تنوع محصول
                        await _ProductVariantItemsApplication.RemoveAllVariantsFromProductAsync(new InpRemoveVariantsFromProduct() { ProductId = ProductId });

                        // حذف کلمات کلیدی
                        await _KeywordProductsApplication.RemoveAllProductKeywordsAsync(new InpRemoveAllProductKeywords() { ProductId = ProductId });

                        // حذف خصوصیات
                        await _ProductPropertiesValuesApplication.RemovePropertiesByProductIdAsync(new InpRemovePropertiesByProductId() { ProductId = ProductId });

                        // حذف محصول
                        await _ProductRepository.DeleteAsync(Guid.Parse(ProductId), default, true);

                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region ثبت تصاویر

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
