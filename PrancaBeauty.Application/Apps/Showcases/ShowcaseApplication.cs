﻿using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Showcases
{
    public class ShowcaseApplication : IShowcaseApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseRepository _ShowcaseRepository;

        public ShowcaseApplication(ILogger logger, IServiceProvider serviceProvider, IShowcaseRepository showcaseRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShowcaseRepository=showcaseRepository;
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
                    Input.StartDate=DateTime.Now;

                if (Input.EndDate.HasValue)
                    if (Input.StartDate >= Input.EndDate.Value)
                        return new OperationResult().Failed("EndDateMustBeGreaterThanStartDate");
                #endregion

                #region Check Name duplicate
                if (await _ShowcaseRepository.Get.AnyAsync(a => a.Name==Input.Name))
                    return new OperationResult().Failed("NameIsDuplicate");

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
                        Sort=0,
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
                    return new OperationResult().Failed("IdNotFound");

                if (qData.HasTab)
                    return new OperationResult().Failed("ShowCaseHasTab.PleaseRemove");

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
                    return new OperationResult().Failed("IdNotFound");

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
                    return null;

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
    }
}
