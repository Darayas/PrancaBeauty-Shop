using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.Application.Apps.Currency;
using PrancaBeauty.Application.Apps.KeywordsProducts;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.PostingRestrictions;
using PrancaBeauty.Application.Apps.ProductMedia;
using PrancaBeauty.Application.Apps.ProductPrices;
using PrancaBeauty.Application.Apps.ProductPropertiesValues;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Apps.Seller;
using PrancaBeauty.Application.Contracts.Currency;
using PrancaBeauty.Application.Contracts.KeywordProducts;
using PrancaBeauty.Application.Contracts.Languages;
using PrancaBeauty.Application.Contracts.PostingRestrictions;
using PrancaBeauty.Application.Contracts.ProductMedia;
using PrancaBeauty.Application.Contracts.ProductPrice;
using PrancaBeauty.Application.Contracts.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Contracts.Sellers;
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
        private readonly IProductPriceApplication _ProductPriceApplication;
        private readonly IProductRepository _ProductRepository;
        private readonly ICategoryApplication _CategoryApplication;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;
        private readonly IProductPropertiesValuesApplication _ProductPropertiesValuesApplication;
        private readonly IKeywordProductsApplication _KeywordProductsApplication;
        private readonly IPostingRestrictionsApplication _PostingRestrictionsApplication;
        private readonly ICurrencyApplication _CurrencyApplication;
        private readonly ILanguageApplication _LanguageApplication;
        private readonly IProductMediaApplication _ProductMediaApplication;
        private readonly IProductSellersApplication _ProductSellersApplication;
        private readonly ISellerApplication _SellersApplication;

        public ProductApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IProductPriceApplication productPriceApplication, IProductRepository productRepository, ICategoryApplication categoryApplication, IProductVariantItemsApplication productVariantItemsApplication, IProductPropertiesValuesApplication productPropertiesValuesApplication, IKeywordProductsApplication keywordProductsApplication, IPostingRestrictionsApplication postingRestrictionsApplication, ICurrencyApplication currencyApplication, ILanguageApplication languageApplication, IProductMediaApplication productMediaApplication, IProductSellersApplication productSellersApplication, ISellerApplication sellersApplication)
        {
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
            _ProductPriceApplication = productPriceApplication;
            _ProductRepository = productRepository;
            _CategoryApplication = categoryApplication;
            _ProductVariantItemsApplication = productVariantItemsApplication;
            _ProductPropertiesValuesApplication = productPropertiesValuesApplication;
            _KeywordProductsApplication = keywordProductsApplication;
            _PostingRestrictionsApplication = postingRestrictionsApplication;
            _CurrencyApplication = currencyApplication;
            _LanguageApplication = languageApplication;
            _ProductMediaApplication = productMediaApplication;
            _ProductSellersApplication = productSellersApplication;
            _SellersApplication = sellersApplication;
        }

        public async Task<(OutPagingData, List<OutGetProductsForManage>)> GetProductsForManageAsync(InpGetProductsForManage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _ProductRepository.Get
                                              .Where(a => Input.SellerId != null ? a.tblProductSellers.Where(b => b.tblSellers.Id == Guid.Parse(Input.SellerId)).Any() : true)
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
                                                  Status = a.IsDelete ? OutGetProductsForManage_Status.IsDelete : (a.Incomplete ? OutGetProductsForManage_Status.InComplete : (a.IsDraft ? OutGetProductsForManage_Status.IsDraft : (a.IsConfirmed ? OutGetProductsForManage_Status.IsConfirm : (a.ItsForConfirm ? OutGetProductsForManage_Status.ItsForConfirm : (a.Date > DateTime.Now ? OutGetProductsForManage_Status.IsSchedule : OutGetProductsForManage_Status.UnKnown))))),
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
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
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
                if (await CheckDuplicateNameAsync(Input.Name.ToLowerCaseForUrl()))
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
                        Name = Input.Name.ToLowerCaseForUrl(),
                        Title = Input.Title,
                        Date = Input.Date == null ? DateTime.Now : (Convert.ToDateTime(Input.Date).AddHours(1) < DateTime.Now ? DateTime.Now : Convert.ToDateTime(Input.Date)),
                        IsConfirmed = false,
                        IsDelete = false,
                        ItsForConfirm = true,
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
                        await RemoveProductForAlwaysAsync(new InpRemoveProductForAlways { ProductId = ProductId });

                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region کلمات کلیدی
                {
                    var _Result = await _KeywordProductsApplication.AddKeywordsToProductAsync(new InpAddKeywordsToProduct() { ProductId = ProductId, LstKeywords = Input.Keywords.Select(a => new InpAddKeywordsToProduct_LstKeywords() { Title = a.Title, Similarity = a.Similarity }).ToList() });
                    if (!_Result.IsSucceeded)
                    {
                        await RemoveProductForAlwaysAsync(new InpRemoveProductForAlways { ProductId = ProductId });

                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region ثبت فروشنده ی محصول
                string _ProductSellerId = null;
                {
                    var _Result = await _ProductSellersApplication.AddSellerToProdcutAsync(new InpAddSellerToProdcut
                    {
                        ProductId = ProductId,
                        SellerId = await _SellersApplication.GetSellerIdByUserIdAsync(new InpGetSellerIdByUserId { UserId = Input.AuthorUserId }),
                        IsConfirm = true
                    });

                    if (_Result.IsSucceeded)
                    {
                        _ProductSellerId = _Result.Message;
                    }
                    else
                    {
                        await RemoveProductForAlwaysAsync(new InpRemoveProductForAlways { ProductId = ProductId });

                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region تنوع محصول
                {
                    var _Result = await _ProductVariantItemsApplication.AddVariantsToProductAsync(new InpAddVariantsToProduct
                    {
                        ProductId = ProductId,
                        ProductSellerId = _ProductSellerId,
                        VariantId = Input.VariantId,
                        Variants = Input.Variants.Select(a => new InpAddVariantsToProduct_Variants
                        {
                            CountInStock = a.CountInStock,
                            GuaranteeId = a.GuaranteeId,
                            IsEnable = a.IsEnable,
                            Percent = a.Percent,
                            ProductCode = a.ProductCode,
                            SendBy = a.SendBy,
                            SendFrom = a.SendFrom,
                            Title = a.Title,
                            Value = a.Value
                        }).ToList()
                    });
                    if (!_Result.IsSucceeded)
                    {
                        await RemoveProductForAlwaysAsync(new InpRemoveProductForAlways { ProductId = ProductId });

                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region محدودیت های ارسال پستی
                {
                    var _Result = await _PostingRestrictionsApplication.AddPostingRestrictionsToProductAsync(new InpAddPostingRestrictionsToProduct
                    {
                        ProductId = ProductId,
                        PostingRestrictions = Input.PostingRestrictions.Select(a => new InpAddPostingRestrictionsToProduct_Restrictions
                        {
                            CountryId = a.CountryId,
                            Posting = a.Posting
                        }).ToList()
                    });

                    if (_Result.IsSucceeded == false)
                    {
                        await RemoveProductForAlwaysAsync(new InpRemoveProductForAlways { ProductId = ProductId });

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
                        await RemoveProductForAlwaysAsync(new InpRemoveProductForAlways { ProductId = ProductId });

                        return new OperationResult().Failed("Error500");
                    }
                }
                #endregion

                #region ثبت تصاویر
                {
                    var _Result = await _ProductMediaApplication.AddMediasToProductAsync(new InpAddMediasToProduct() { ProductId = ProductId, MediaIds = Input.ProductImagesId });
                    if (_Result.IsSucceeded == false)
                    {
                        await RemoveProductForAlwaysAsync(new InpRemoveProductForAlways { ProductId = ProductId });
                    }

                }
                #endregion

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> RemoveProductForAlwaysAsync(InpRemoveProductForAlways Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                var qProduct = await _ProductRepository.GetById(default, Guid.Parse(Input.ProductId));
                if (qProduct == null)
                    return new OperationResult().Failed("IdIsInvalid");

                if (Input.AuthorUserId != null)
                    if (qProduct.AuthorUserId != Guid.Parse(Input.AuthorUserId))
                        return new OperationResult().Failed("AccessDenied");
                #endregion

                // TODO برسی نبودن سفارش ثبت شده برای محصول جاری

                // TODO حذف کامنت ها

                // TODO حذف پرسش ها

                // حذف تصاویر محصول
                await _ProductMediaApplication.RemoveAllMediaFromProductAsync(new InpRemoveAllMediaFromProduct { ProductId = Input.ProductId });

                // حذف قیمت محصول
                await _ProductPriceApplication.RemovePriceFromProductAsync(new InpRemovePriceFromProduct { ProductId = Input.ProductId });

                // حذف محدودیت های ارسال
                await _PostingRestrictionsApplication.RemoveAllPostingRestrictionsFromProductAsync(new InpRemoveAllPostingRestrictionsFromProduct { ProductId = Input.ProductId });

                // حذف تنوع محصول
                await _ProductVariantItemsApplication.RemoveAllVariantsFromProductAsync(new InpRemoveVariantsFromProduct() { ProductId = Input.ProductId });

                // حذف کلمات کلیدی
                await _KeywordProductsApplication.RemoveAllProductKeywordsAsync(new InpRemoveAllProductKeywords() { ProductId = Input.ProductId });

                // حذف خصوصیات
                await _ProductPropertiesValuesApplication.RemovePropertiesByProductIdAsync(new InpRemovePropertiesByProductId() { ProductId = Input.ProductId });

                // حذف محصول
                await _ProductRepository.DeleteAsync(qProduct, default, true);

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> MoveToRecycleBinAsync(InpMoveToRecycleBin Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                var qProduct = await _ProductRepository.GetById(default, Guid.Parse(Input.ProductId));
                if (qProduct == null)
                    return new OperationResult().Failed("IdIsInvalid");

                if (Input.AuthorUserId != null)
                    if (qProduct.AuthorUserId != Guid.Parse(Input.AuthorUserId))
                        return new OperationResult().Failed("AccessDenied");
                #endregion

                qProduct.IsDelete = true;
                qProduct.IsConfirmed = false;
                qProduct.ItsForConfirm = true;

                await _ProductRepository.UpdateAsync(qProduct, default, true);

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> RecoveryFromRecycleBinAsync(InpRecoveryFromRecycleBin Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                var qProduct = await _ProductRepository.GetById(default, Guid.Parse(Input.ProductId));
                if (qProduct == null)
                    return new OperationResult().Failed("IdIsInvalid");

                if (Input.AuthorUserId != null)
                    if (qProduct.AuthorUserId != Guid.Parse(Input.AuthorUserId))
                        return new OperationResult().Failed("AccessDenied");
                #endregion

                qProduct.IsDelete = false;
                qProduct.IsConfirmed = false;
                qProduct.ItsForConfirm = true;

                await _ProductRepository.UpdateAsync(qProduct, default, true);

                return new OperationResult().Succeeded();
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

        public async Task<OutGetForEdit> GetForEditAsync(InpGetForEdit Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                string _CurrencyId = await GetCurrencyIdByProductIdAsync(Input.ProductId);

                var qData = await _ProductRepository.Get
                                                  .Where(a => Input.UserId != null ? a.AuthorUserId == Guid.Parse(Input.UserId) : true)
                                                  .Where(a => a.Id == Guid.Parse(Input.ProductId))
                                                  .Select(a => new OutGetForEdit
                                                  {
                                                      Id = a.Id.ToString(),
                                                      AuthorUserId = a.AuthorUserId.ToString(),
                                                      LangId = a.LangId.ToString(),
                                                      CategoryId = a.CategoryId.ToString(),
                                                      TopicId = a.TopicId.ToString(),
                                                      Name = a.Name,
                                                      Title = a.Title,
                                                      MetaTagCanonical = a.MetaTagCanonical,
                                                      MetaTagDescreption = a.MetaTagDescreption,
                                                      Description = a.Description,
                                                      IsDraft = a.IsDraft,
                                                      Date = a.Date,
                                                      MetaTagKeyword = a.MetaTagKeyword,
                                                      Price = a.tblProductPrices.Where(a => a.IsActive).Where(a => a.CurrencyId == Guid.Parse(_CurrencyId)).Select(b => b.Price).Single(),
                                                      ProductImagesId = string.Join(",", a.tblProductMedia.Select(a => a.FileId).Single())
                                                  })
                                                  .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        private async Task<string> GetCurrencyIdByProductIdAsync(string ProductId)
        {
            string _LangId = await _ProductRepository.Get.Where(a => a.Id == Guid.Parse(ProductId)).Select(a => a.LangId.ToString()).SingleAsync();

            string _CurrencyId = await _CurrencyApplication.GetIdByCountryIdAsync(new InpGetIdByCountryId
            {
                CountryId = await _LanguageApplication.GetCountryIdByLangIdAsync(new InpGetCountryIdByLangId
                {
                    LangId = _LangId
                })
            });

            return _CurrencyId;
        }

        public async Task<OperationResult> SaveEditProductAsync(InpSaveEditProduct Input)
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
                //if (await CheckDuplicateNameAsync(Input.Name.ToLowerCaseForUrl(), Input.Id))
                //    return new OperationResult().Failed("ProdcutName is duplicate.");

                var qData = await _ProductRepository.Get
                                                    .Where(a => a.Id == Guid.Parse(Input.Id))
                                                    //.Where(a => Input.EditorUserId != null ? a.AuthorUserId == Guid.Parse(Input.EditorUserId) : true)
                                                    .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("ProductNotFound");

                if (qData.IsDraft == false && Input.IsDraft == true)
                    return new OperationResult().Failed("YouCantEditThisProductAsDraft");

                if (Input.CanEditThisProduct == false)
                    if (qData.AuthorUserId.ToString().ToLower() != Input.EditorUserId.ToLower())
                        return new OperationResult().Failed("YouCantEditThisProduct");

                qData.TopicId = Guid.Parse(Input.TopicId);
                qData.CategoryId = Guid.Parse(Input.CategoryId);
                qData.Title = Input.Title;
                qData.Date = Input.Date == null ? DateTime.Now : (Convert.ToDateTime(Input.Date).AddHours(1) < DateTime.Now ? DateTime.Now : Convert.ToDateTime(Input.Date));
                qData.MetaTagCanonical = Input.MetaTagCanonical;
                qData.MetaTagKeyword = Input.MetaTagKeyword;
                qData.MetaTagDescreption = Input.MetaTagDescreption;
                qData.Description = Input.Description.GetSanitizeHtml();
                qData.IsConfirmed = false;
                qData.IsDraft = Input.IsDraft;
                qData.ItsForConfirm = true;
                qData.Incomplete = false;
                qData.IncompleteReason = null;

                #region ویرایش قیمت پایه
                {
                    var _Result = await _ProductPriceApplication.AddPriceToProductAsyc(new InpAddPriceToProduct
                    {
                        UserId = Input.EditorUserId,
                        ProductId = Input.Id,
                        CurrencyId = await _CurrencyApplication.GetIdByCountryIdAsync(new InpGetIdByCountryId
                        {
                            CountryId = await _LanguageApplication.GetCountryIdByLangIdAsync(new InpGetCountryIdByLangId
                            {
                                LangId = qData.LangId.ToString()
                            })
                        }),
                        Price = Input.Price
                    });
                    if (_Result.IsSucceeded == false)
                    {

                        await SetInCompleteAsync(Input.Id, _Localizer["InCompleteProductReason", Input.Id, _Result.Message]);

                        return new OperationResult().Failed(_Result.Message);
                    }
                }
                #endregion

                #region ویرایش خصوصیات
                {
                    var _Result = await _ProductPropertiesValuesApplication.EditProductPropertiesAsync(new InpEditProductProperties
                    {
                        ProductId = Input.Id,
                        PropItems = Input.Properties.Select(a => new InpEditProductProperties_Items
                        {
                            Id = a.Id,
                            Value = a.Value
                        }).ToList()
                    });
                    if (_Result.IsSucceeded == false)
                    {
                        await SetInCompleteAsync(Input.Id, _Localizer["InCompleteProductReason", Input.Id, _Result.Message]);

                        return new OperationResult().Failed(_Result.Message);
                    }
                }
                #endregion

                #region ویرایش کلمات کلیدی
                {
                    var _Result = await _KeywordProductsApplication.EditProductKeywordsAsync(new InpEditProductKeywords
                    {
                        ProductId = Input.Id,
                        LstKeywords = Input.Keywords.Where(a => a.Title != null).Where(a => a.IsDelete == false).Select(a => new InpEditProductKeywords_LstKeywords
                        {
                            Title = a.Title,
                            Similarity = a.Similarity
                        }).ToList()
                    });
                    if (_Result.IsSucceeded == false)
                    {
                        await SetInCompleteAsync(Input.Id, _Localizer["InCompleteProductReason", Input.Id, _Result.Message]);

                        return new OperationResult().Failed(_Result.Message);
                    }
                }
                #endregion

                #region ویرایش محدودیت های ارسال
                {
                    var _Result = await _PostingRestrictionsApplication.EditPostingRestrictionsAsync(new InpEditPostingRestrictions
                    {
                        ProductId = Input.Id,
                        PostingRestrictions = Input.PostingRestrictions.Select(a => new InpEditPostingRestrictions_Restrictions
                        {
                            CountryId = a.CountryId,
                            Posting = a.Posting
                        }).ToList()
                    });

                    if (_Result.IsSucceeded == false)
                    {
                        await SetInCompleteAsync(Input.Id, _Localizer["InCompleteProductReason", Input.Id, _Result.Message]);

                        return new OperationResult().Failed(_Result.Message);
                    }
                }
                #endregion

                #region ویرایش تصاویر
                {
                    var _Result = await _ProductMediaApplication.EditProductMediaAsync(new InpEditProductMedia
                    {
                        ProductId = Input.Id,
                        MediaIds = Input.ProductImagesId
                    });

                    if (_Result.IsSucceeded == false)
                    {
                        await SetInCompleteAsync(Input.Id, _Localizer["InCompleteProductReason", Input.Id, _Result.Message]);

                        return new OperationResult().Failed(_Result.Message);
                    }
                }
                #endregion

                #region ثبت فروشنده ی محصول
                string _ProductSellerId = null;
                {
                    _ProductSellerId = await _ProductSellersApplication.GetProductSellerIdAsync(new InpGetProductSellerId { ProductId = qData.Id.ToString(), UserId = qData.AuthorUserId.ToString() });
                    if (_ProductSellerId == null)
                    {
                        var _Result = await _ProductSellersApplication.AddSellerToProdcutAsync(new InpAddSellerToProdcut
                        {
                            ProductId = qData.Id.ToString(),
                            SellerId = await _SellersApplication.GetSellerIdByUserIdAsync(new InpGetSellerIdByUserId { UserId = qData.AuthorUserId.ToString() }),
                            IsConfirm = true
                        });

                        if (_Result.IsSucceeded)
                        {
                            _ProductSellerId = _Result.Message;
                        }
                        else
                        {
                            return new OperationResult().Failed(_Result.Message);
                        }
                    }
                }
                #endregion

                #region ویرایش تنوع محصول
                {
                    var _Result = await _ProductVariantItemsApplication.EditProductVariantsAsync(new InpEditProductVariants
                    {
                        ProductId = Input.Id,
                        ProductSellerId = _ProductSellerId,
                        VariantId = Input.VariantId,
                        Variants = Input.Variants.Select(a => new InpEditProductVariants_Variants
                        {
                            Id = a.Id,
                            GuaranteeId = a.GuaranteeId,
                            ProductCode = a.ProductCode,
                            Title = a.Title,
                            Value = a.Value,
                            CountInStock = a.CountInStock,
                            IsDelete = a.IsDelete,
                            IsEnable = a.IsEnable,
                            Percent = a.Percent,
                            SendBy = a.SendBy,
                            SendFrom = a.SendFrom
                        }).ToList()
                    });

                    if (_Result.IsSucceeded == false)
                    {
                        await SetInCompleteAsync(Input.Id, _Localizer["InCompleteProductReason", Input.Id, _Result.Message]);

                        return new OperationResult().Failed(_Result.Message);
                    }
                }
                #endregion

                return new OperationResult().Succeeded();
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

        private async Task<OperationResult> SetInCompleteAsync(string ProductId, string Reason)
        {
            try
            {
                var qData = await _ProductRepository.Get
                                                    .Where(a => a.Id == Guid.Parse(ProductId))
                                                    .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("IdNotFound");

                qData.Incomplete = true;
                qData.IncompleteReason = Reason;
                qData.IsConfirmed = false;

                await _ProductRepository.SaveChangeAsync();

                return new OperationResult().Succeeded();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<string> GetTitleByIdAsync(InpGetTitleById Input)
        {
            try
            {
                #region Validatons
                Input.CheckModelState(_ServiceProvider);
                #endregion

                return await _ProductRepository.Get
                                               .Where(a => a.Id == Guid.Parse(Input.ProductId))
                                               .Select(a => a.Title)
                                               .SingleOrDefaultAsync();

            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }


    }
}
