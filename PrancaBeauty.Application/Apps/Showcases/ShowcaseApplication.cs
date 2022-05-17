using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Domain.Enums;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Common.ExMethods;
using PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Entities;

namespace PrancaBeauty.Application.Apps.Showcases
{
    public class ShowcaseApplication : IShowcaseApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseRepository _ShowcaseRepository;
        private readonly IShowcaseTranslateRepository _ShowcaseTranslateRepository;

        public ShowcaseApplication(ILogger logger, IServiceProvider serviceProvider, IShowcaseRepository showcaseRepository, IShowcaseTranslateRepository showcaseTranslateRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShowcaseRepository=showcaseRepository;
            _ShowcaseTranslateRepository=showcaseTranslateRepository;
        }

        public async Task<(OutPagingData, List<OutGetListShowcaseForAdminPage>)> GetListShowcaseForAdminPageAsync(InpGetListShowcaseForAdminPage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _ShowcaseRepository.Get
                                               .Select(a => new OutGetListShowcaseForAdminPage
                                               {
                                                   Id=a.Id.ToString(),
                                                   Name=a.Name,
                                                   Title=a.tblShowcasesTranslates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).SingleOrDefault(),
                                                   CountyTitle=a.CountryId!=null ? a.tblCountry.tblCountries_Translates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single() : "",
                                                   IsFullWidth=a.IsFullWidth,
                                                   IsEnable=a.IsEnable,
                                                   IsActive=a.IsActive,
                                                   StartDate=a.StartDate,
                                                   EndDate=a.EndDate,
                                                   Sort=a.Sort
                                               })
                                               .Where(a => Input.Title!=null ? a.Title.Contains(Input.Title) : true)
                                               .OrderBy(a => a.Sort);

                var _PagingData = PagingData.Calc(await qData.LongCountAsync(), Input.Page, Input.Take);
                return (_PagingData, await qData.Skip((int)_PagingData.Skip).Take(_PagingData.Take).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }

        public async Task<OperationResult> AddShowcaseAsync(InpAddShowcase Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                if (Input.StartDate==null)
                {
                    Input.StartDate=DateTime.Now;
                }

                if (Input.EndDate.HasValue)
                {
                    if (Input.StartDate >= Input.EndDate.Value)
                    {
                        return new OperationResult().Failed("EndDateMustBeGreaterThanStartDate");
                    }
                }
                #endregion

                #region Check Name duplicate
                if (await _ShowcaseRepository.Get.AnyAsync(a => a.Name==Input.Name))
                {
                    return new OperationResult().Failed("NameIsDuplicate");
                }

                #endregion

                #region Get SortNum
                int _Sort = 0;
                {
                    _Sort= await _ShowcaseRepository.Get.CountAsync();
                }
                #endregion

                #region Add Showcase
                {
                    var tShowcase = new tblShowcases
                    {
                        Id= new Guid().SequentialGuid(),
                        Name=Input.Name,
                        CountryId=Input.CountryId!=null ? Input.CountryId.ToGuid() : null,
                        UserId=Input.UserId.ToGuid(),
                        BackgroundColorCode=Input.BackgroundColorCode,
                        CssClass=Input.CssClass,
                        CssStyle=Input.CssStyle,
                        StartDate=Input.StartDate.Value,
                        EndDate=Input.EndDate,
                        IsActive=Input.StartDate<DateTime.Now ? true : false,
                        IsEnable=Input.IsEnable,
                        IsFullWidth=Input.IsFullWidth,
                        Sort=_Sort,
                        tblShowcasesTranslates= Input.LstTranslate.Select(a => new tblShowcasesTranslates
                        {
                            Id= new Guid().SequentialGuid(),
                            LangId=a.LangId.ToGuid(),
                            Title=a.Title,
                            Description=a.Description
                        }).ToList()
                    };

                    await _ShowcaseRepository.AddAsync(tShowcase, default, true);
                }
                #endregion

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> RemoveShowcaseAsync(InpRemoveShowcase Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseRepository.Get
                                                     .Where(a => a.Id==Input.Id.ToGuid())
                                                     .Select(a => new
                                                     {
                                                         HasTab = a.tblShowcaseTabs.Any(),
                                                         Showcase = a
                                                     })
                                                     .SingleOrDefaultAsync();

                if (qData==null)
                {
                    return new OperationResult().Failed("IdNotFound");
                }

                if (qData.HasTab)
                {
                    return new OperationResult().Failed("ShowCaseHasTab.PleaseRemove");
                }

                await _ShowcaseRepository.DeleteAsync(qData.Showcase, default, true);
                return new OperationResult().Succeeded();

            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> SortingShowcaseAsync(InpSortingShowcase Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qListSlide = await _ShowcaseRepository.Get.OrderBy(a => a.Sort).ToListAsync();
                var qCurrentItem = qListSlide.Where(a => a.Id==Input.Id.ToGuid()).SingleOrDefault();
                if (qCurrentItem==null)
                {
                    return new OperationResult().Failed("IdNotFound");
                }

                int IndexOfCurrentItem = qListSlide.IndexOf(qCurrentItem);

                if (Input.Act==InpSortingShowcaseSortingItem.Up)
                {
                    if (IndexOfCurrentItem!=0)
                    {
                        var PriveItem = qListSlide[IndexOfCurrentItem-1];

                        int OldPriveIndex = PriveItem.Sort;
                        PriveItem.Sort=IndexOfCurrentItem;
                        qCurrentItem.Sort=OldPriveIndex;

                        await _ShowcaseRepository.UpdateAsync(PriveItem, default, false);
                        await _ShowcaseRepository.UpdateAsync(qCurrentItem, default, false);

                        await _ShowcaseRepository.SaveChangeAsync();
                    }
                }
                else if (Input.Act==InpSortingShowcaseSortingItem.Down)
                {
                    if (IndexOfCurrentItem<(qListSlide.Count()-1))
                    {
                        var NextItem = qListSlide[IndexOfCurrentItem+1];

                        int OldPriveIndex = NextItem.Sort;
                        NextItem.Sort=IndexOfCurrentItem;
                        qCurrentItem.Sort=OldPriveIndex;

                        await _ShowcaseRepository.UpdateAsync(NextItem, default, false);
                        await _ShowcaseRepository.UpdateAsync(qCurrentItem, default, false);

                        await _ShowcaseRepository.SaveChangeAsync();
                    }
                }


                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OutGetShowcaseForEdit> GetShowcaseForEditAsync(InpGetShowcaseForEdit Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseRepository.Get
                                            .Where(a => a.Id==Input.Id.ToGuid())
                                            .Select(a => new OutGetShowcaseForEdit
                                            {
                                                Id=a.Id.ToString(),
                                                CountryId=a.CountryId.ToString(),
                                                Name=a.Name,
                                                BackgroundColorCode=a.BackgroundColorCode,
                                                CssClass=a.CssClass,
                                                CssStyle=a.CssStyle,
                                                IsEnable=a.IsEnable,
                                                IsFullWidth=a.IsFullWidth,
                                                StartDate=a.StartDate,
                                                EndDate=a.EndDate,
                                                LstTranslate=a.tblShowcasesTranslates.Select(a => new OutGetShowcaseForEdit_Translate
                                                {
                                                    LangId=a.LangId.ToString(),
                                                    Title=a.Title,
                                                    Description=a.Description,
                                                }).ToList()
                                            })
                                            .SingleOrDefaultAsync();

                if (qData==null)
                {
                    return null;
                }

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }

        public async Task<OperationResult> SaveEditShowcaseAsync(InpSaveEditShowcase Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                if (Input.StartDate==null)
                {
                    Input.StartDate=DateTime.Now;
                }

                if (Input.EndDate.HasValue)
                {
                    if (Input.StartDate >= Input.EndDate.Value)
                    {
                        return new OperationResult().Failed("EndDateMustBeGreaterThanStartDate");
                    }
                }
                #endregion

                #region Check Name duplicate
                if (await _ShowcaseRepository.Get.Where(a => a.Id!=Input.Id.ToGuid()).AnyAsync(a => a.Name==Input.Name))
                {
                    return new OperationResult().Failed("NameIsDuplicate");
                }

                #endregion

                #region Edit showcase
                {
                    var qData = await _ShowcaseRepository.Get
                                            .Where(a => a.Id==Input.Id.ToGuid())
                                            .SingleOrDefaultAsync();

                    if (qData==null)
                    {
                        return new OperationResult().Failed("IdNotFound");
                    }

                    qData.CountryId=Input.CountryId!=null ? Input.CountryId.ToGuid() : null;
                    qData.UserId=Input.UserId.ToGuid();
                    qData.Name=Input.Name;
                    qData.IsFullWidth=Input.IsFullWidth;
                    qData.BackgroundColorCode= Input.BackgroundColorCode;
                    qData.CssClass=Input.CssClass;
                    qData.CssStyle=Input.CssStyle;
                    qData.EndDate=Input.EndDate;
                    qData.StartDate=Input.StartDate.Value;
                    qData.IsEnable=Input.IsEnable;
                    qData.IsActive=Input.StartDate<DateTime.Now ? true : false;

                    await _ShowcaseRepository.UpdateAsync(qData, default, true);

                }
                #endregion

                #region Edit showcase translate
                {
                    #region Remove old translates
                    {
                        var qOldData = await _ShowcaseTranslateRepository.Get
                                                        .Where(a => a.ShowcaseId==Input.Id.ToGuid())
                                                        .ToListAsync();

                        await _ShowcaseTranslateRepository.DeleteRangeAsync(qOldData, default, true);
                    }
                    #endregion

                    #region Add new translates
                    {
                        var qNewData = Input.LstTranslate.Select(b => new tblShowcasesTranslates
                        {
                            Id= new Guid().SequentialGuid(),
                            ShowcaseId=Input.Id.ToGuid(),
                            LangId=b.LangId.ToGuid(),
                            Title=b.Title,
                            Description=b.Description
                        });

                        await _ShowcaseTranslateRepository.AddRangeAsync(qNewData, default, true);
                    }
                    #endregion
                }
                #endregion

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<List<OutGetItemsForMainPageShowcase>> GetItemsForMainPageShowcaseAsync(InpGetItemsForMainPageShowcase Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseRepository.Get
                                        .Where(a => a.IsActive)
                                        .Where(a => a.IsEnable)
                                        .Where(a => a.tblShowcaseTabs.Count()>0)
                                        .OrderBy(a => a.Sort)
                                        .Where(a => a.CountryId != null ? a.CountryId == Input.CountryId.ToGuid() : true)
                                        .Select(a => new OutGetItemsForMainPageShowcase
                                        {
                                            Title=a.tblShowcasesTranslates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single(),
                                            Description=a.tblShowcasesTranslates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Description).Single(),
                                            BackgroundColorCode=a.BackgroundColorCode,
                                            CssClass=a.CssClass,
                                            CssStyle=a.CssStyle,
                                            IsFullWidth=a.IsFullWidth,
                                            LstTabs= a.tblShowcaseTabs
                                                        .Where(b => b.IsEnable)
                                                        .Where(b => b.IsActive)
                                                        .Where(b => b.tblShowcaseTabSections.Count()>0)
                                                        .OrderBy(b => b.Sort)
                                                        .Select(b => new OutGetItemsForMainPageShowcase_Tab
                                                        {
                                                            Title=b.tblShowcaseTabTranslates.Where(c => c.LangId==Input.LangId.ToGuid()).Select(c => c.Title).Single(),
                                                            BackgroundColorCode=b.BackgroundColorCode,
                                                            StartDate=b.StartDate,
                                                            EndDate=b.EndDate,
                                                            LstTabSection = b.tblShowcaseTabSections
                                                                                .Select(c => new OutGetItemsForMainPageShowcase_TabSection
                                                                                {
                                                                                    Id=c.Id.ToString(),
                                                                                    ParentId=c.ParentId!=null ? c.ParentId.ToString() : null,
                                                                                    HowToDisplayItems=c.HowToDisplayItems,
                                                                                    CountInSection=c.CountInSection,
                                                                                    IsSlider=c.IsSlider,
                                                                                    XlSize=c.XlSize,
                                                                                    LgSize=c.LgSize,
                                                                                    MdSize=c.MdSize,
                                                                                    SmSize=c.SmSize,
                                                                                    XsSize=c.XsSize,
                                                                                    LstSectionItem= c.tblShowcaseTabSectionItems
                                                                                                        .Select(d => new OutGetItemsForMainPageShowcase_SectionItem
                                                                                                        {
                                                                                                            SectionType=d.SectionType,
                                                                                                            FreeItem= d.SectionType==TabSectionItemsEnum.FreeItem ? new OutGetItemsForMainPageShowcase_SectionFreeItem
                                                                                                            {
                                                                                                                Title=d.tblSectionFreeItems.tblSectionFreeItemTranslate.Where(e => e.LangId==Input.LangId.ToGuid()).Select(e => e.Title).Single(),
                                                                                                                Url=d.tblSectionFreeItems.tblSectionFreeItemTranslate.Where(e => e.LangId==Input.LangId.ToGuid()).Select(e => e.Url).Single(),
                                                                                                                HtmlText=d.tblSectionFreeItems.tblSectionFreeItemTranslate.Where(e => e.LangId==Input.LangId.ToGuid()).Select(e => e.HtmlText).Single(),
                                                                                                                ImgUrl=d.tblSectionFreeItems.tblSectionFreeItemTranslate.Where(e => e.LangId==Input.LangId.ToGuid()).Select(e => new
                                                                                                                {
                                                                                                                    ImgUrl = e.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                                                                              + e.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                                                                              + e.tblFiles.tblFilePaths.Path
                                                                                                                              + e.tblFiles.FileName
                                                                                                                }).Single().ImgUrl,
                                                                                                            } : null,
                                                                                                            ProductItem= d.SectionType==TabSectionItemsEnum.Product ? new OutGetItemsForMainPageShowcase_SectionProductItem
                                                                                                            {
                                                                                                                Id=d.tblSectionProducts.ProductId.ToString(),
                                                                                                                Title =d.tblSectionProducts.tblProducts.Title,
                                                                                                                Name=d.tblSectionProducts.tblProducts.Name,
                                                                                                                MainPrice= d.tblSectionProducts.tblProducts.tblProductPrices.Where(e => e.CurrencyId==Input.CurrencyId.ToGuid()).Where(e => e.IsActive).Select(e => e.Price).First(),
                                                                                                                SellerPercent= d.tblSectionProducts.tblProducts.tblProductVariantItems.Where(d => d.IsEnable && d.IsConfirm && d.CountInStock>0).Select(e => new { SellerPercent = e.Percent - (e.tblProductDiscounts!=null ? e.tblProductDiscounts.Percent : 0), Percent = e.Percent }).OrderBy(e => e.SellerPercent).FirstOrDefault().Percent,
                                                                                                                PercentSavePrice= d.tblSectionProducts.tblProducts.tblProductVariantItems.Where(d => d.IsEnable && d.IsConfirm && d.CountInStock>0).Select(e => new { SellerPercent = e.Percent - (e.tblProductDiscounts!=null ? e.tblProductDiscounts.Percent : 0), SavePercent = (e.tblProductDiscounts!=null ? e.tblProductDiscounts.Percent : 0) }).OrderBy(e => e.SellerPercent).FirstOrDefault().SavePercent,
                                                                                                                CurrencySymbol=d.tblSectionProducts.tblProducts.tblProductPrices.Where(e => e.CurrencyId==Input.CurrencyId.ToGuid()).Where(e => e.IsActive).Select(e => e.tblCurrency.Symbol).First(),
                                                                                                                IsInBookmark=false,// TODO: Create Bookmark Table
                                                                                                                ImgUrl=d.tblSectionProducts.tblProducts.tblProductMedia.Select(e => new
                                                                                                                {
                                                                                                                    ImgUrl = e.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                                                                              + e.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                                                                              + e.tblFiles.tblFilePaths.Path
                                                                                                                              + e.tblFiles.FileName
                                                                                                                }).Select(d => d.ImgUrl).Take(2).ToArray(),
                                                                                                            } : null,
                                                                                                            CategoryItems= d.SectionType==TabSectionItemsEnum.Category ? new OutGetItemsForMainPageShowcase_SectionCategory
                                                                                                            {
                                                                                                                CountFetch= d.tblSectionProductCategory.CountFetch,
                                                                                                                LstProducts= d.tblSectionProductCategory.tblCategory.tblProducts.Where(e => e.IsConfirmed)
                                                                                                                            .OrderBy(e => d.tblSectionProductCategory.OrderBy==tblSectionProductCategoryOrderByEnum.Newest ? e.Date : default)
                                                                                                                            .ThenByDescending(e => d.tblSectionProductCategory.OrderBy==tblSectionProductCategoryOrderByEnum.Oldest ? e.Date : default)
                                                                                                                            .ThenByDescending(e => d.tblSectionProductCategory.OrderBy==tblSectionProductCategoryOrderByEnum.Popular ? e.tblProductReviews.Average(f => f.CountStar) : default)
                                                                                                                            .Select(e => new OutGetItemsForMainPageShowcase_SectionProductItem
                                                                                                                            {
                                                                                                                                Id = e.Id.ToString(),
                                                                                                                                Title = e.Title,
                                                                                                                                Name = e.Name,
                                                                                                                                MainPrice = e.tblProductPrices.Where(f => f.CurrencyId==Input.CurrencyId.ToGuid()).Where(f => f.IsActive).Select(f => f.Price).First(),
                                                                                                                                SellerPercent = e.tblProductVariantItems.Where(d => d.IsEnable && d.IsConfirm && d.CountInStock>0).Select(f => new { SellerPercent = f.Percent - (f.tblProductDiscounts!=null ? f.tblProductDiscounts.Percent : 0), Percent = f.Percent }).OrderBy(f => f.SellerPercent).FirstOrDefault().Percent,
                                                                                                                                PercentSavePrice = e.tblProductVariantItems.Where(d => d.IsEnable && d.IsConfirm && d.CountInStock>0).Select(f => new { SellerPercent = f.Percent - (f.tblProductDiscounts!=null ? f.tblProductDiscounts.Percent : 0), SavePercent = (f.tblProductDiscounts!=null ? f.tblProductDiscounts.Percent : 0) }).OrderBy(f => f.SellerPercent).FirstOrDefault().SavePercent,
                                                                                                                                CurrencySymbol = e.tblProductPrices.Where(e => e.CurrencyId==Input.CurrencyId.ToGuid()).Where(e => e.IsActive).Select(e => e.tblCurrency.Symbol).First(),
                                                                                                                                IsInBookmark = false,// TODO: Create Bookmark Table
                                                                                                                                ImgUrl = e.tblProductMedia.Select(f => new
                                                                                                                                {
                                                                                                                                    ImgUrl = f.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                                                                                                + f.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                                                                                                + f.tblFiles.tblFilePaths.Path
                                                                                                                                                + f.tblFiles.FileName
                                                                                                                                }).Select(f => f.ImgUrl).Take(2).ToArray(),
                                                                                                                            }).Take(50).ToList()
                                                                                                            } : null,
                                                                                                            KeywordItems= d.SectionType==TabSectionItemsEnum.Keyword ? new OutGetItemsForMainPageShowcase_SectionKeyword
                                                                                                            {
                                                                                                                CountFetch= d.tblSectionProductKeyword.CountFetch,
                                                                                                                LstProducts= d.tblSectionProductKeyword.tblKeywords.tblKeywords_Products.Where(e => e.tblProducts.IsConfirmed)
                                                                                                                            .OrderBy(e => d.tblSectionProductKeyword.OrderBy==tblSectionProductKeywordOrderByEnum.Newest ? e.tblProducts.Date : default)
                                                                                                                            .ThenByDescending(e => d.tblSectionProductKeyword.OrderBy==tblSectionProductKeywordOrderByEnum.Oldest ? e.tblProducts.Date : default)
                                                                                                                            .ThenByDescending(e => d.tblSectionProductKeyword.OrderBy==tblSectionProductKeywordOrderByEnum.Popular ? e.tblProducts.tblProductReviews.Average(f => f.CountStar) : default)
                                                                                                                            .Select(e => new OutGetItemsForMainPageShowcase_SectionProductItem
                                                                                                                            {
                                                                                                                                Id=e.tblProducts.Id.ToString(),
                                                                                                                                Title =e.tblProducts.Title,
                                                                                                                                Name=e.tblProducts.Name,
                                                                                                                                MainPrice= e.tblProducts.tblProductPrices.Where(f => f.CurrencyId==Input.CurrencyId.ToGuid()).Where(f => f.IsActive).Select(f => f.Price).First(),
                                                                                                                                SellerPercent= e.tblProducts.tblProductVariantItems.Where(d => d.IsEnable && d.IsConfirm && d.CountInStock>0).Select(f => new { SellerPercent = f.Percent - (f.tblProductDiscounts!=null ? f.tblProductDiscounts.Percent : 0), Percent = f.Percent }).OrderBy(f => f.SellerPercent).FirstOrDefault().Percent,
                                                                                                                                PercentSavePrice= e.tblProducts.tblProductVariantItems.Where(d => d.IsEnable && d.IsConfirm && d.CountInStock>0).Select(f => new { SellerPercent = f.Percent - (f.tblProductDiscounts!=null ? f.tblProductDiscounts.Percent : 0), SavePercent = (f.tblProductDiscounts!=null ? f.tblProductDiscounts.Percent : 0) }).OrderBy(f => f.SellerPercent).FirstOrDefault().SavePercent,
                                                                                                                                CurrencySymbol= e.tblProducts.tblProductPrices.Where(e => e.CurrencyId==Input.CurrencyId.ToGuid()).Where(e => e.IsActive).Select(e => e.tblCurrency.Symbol).First(),
                                                                                                                                IsInBookmark=false,// TODO: Create Bookmark Table
                                                                                                                                ImgUrl=e.tblProducts.tblProductMedia.Select(f => new
                                                                                                                                {
                                                                                                                                    ImgUrl = f.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                                                                                              + f.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                                                                                              + f.tblFiles.tblFilePaths.Path
                                                                                                                                              + f.tblFiles.FileName
                                                                                                                                }).Select(e => e.ImgUrl).Take(2).ToArray()
                                                                                                                            }).Take(50).ToList()
                                                                                                            } : null
                                                                                                        }).ToList()
                                                                                }).ToList()
                                                        }).ToList()
                                        })
                                        .ToListAsync();

                if (qData==null)
                    return null;

                return qData;
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
    }
}
