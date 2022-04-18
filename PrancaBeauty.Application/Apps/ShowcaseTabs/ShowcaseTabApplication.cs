using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShowcaseTabs
{
    public class ShowcaseTabApplication : IShowcaseTabApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseTabsRepository _ShowcaseTabsRepository;
        private readonly IShowcaseTabsTranslateRepository _ShowcaseTabsTranslateRepository;

        public ShowcaseTabApplication(ILogger logger, IServiceProvider serviceProvider, IShowcaseTabsRepository showcaseTabsRepository, IShowcaseTabsTranslateRepository showcaseTabsTranslateRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShowcaseTabsRepository=showcaseTabsRepository;
            _ShowcaseTabsTranslateRepository=showcaseTabsTranslateRepository;
        }

        public async Task<(OutPagingData PagingData, List<OutGetListShowcaseTabForAdminPage> LstData)> GetListShowcaseTabForAdminPageAsync(InpGetListShowcaseTabForAdminPage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _ShowcaseTabsRepository.Get
                                        .Where(a => a.ShowcaseId==Input.ShowcaseId.ToGuid())
                                        .Select(a => new OutGetListShowcaseTabForAdminPage
                                        {
                                            Id=a.Id.ToString(),
                                            Name=a.Name,
                                            Title=a.tblShowcaseTabTranslates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single(),
                                            Sort=a.Sort,
                                            IsEnable=a.IsEnable,
                                            IsActive=a.IsActive,
                                            StartDate=a.StartDate,
                                            EndDate=a.EndDate
                                        })
                                        .OrderBy(a => a.Sort);

                var _PagingData = PagingData.Calc(await qData.LongCountAsync(), Input.Page, Input.Take);
                return (_PagingData, await qData.Skip((int)_PagingData.Skip).Take(_PagingData.Take).ToListAsync());
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

        public async Task<OperationResult> AddShowcaseTabAsync(InpAddShowcaseTab Input)
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

                #region Check name duplicate
                {
                    if (await _ShowcaseTabsRepository.Get.Where(a => a.ShowcaseId==Input.ShowcaseId.ToGuid()).AnyAsync(a => a.Name==Input.Name))
                        return new OperationResult().Failed("NameIsDuplicate");
                }
                #endregion

                #region Check title duplicate
                {
                    var HasDuplicateTitle = (await _ShowcaseTabsTranslateRepository.Get
                                                            .Where(a => a.tblShowcaseTabs.ShowcaseId==Input.ShowcaseId.ToGuid())
                                                            .Select(a => a.Title)
                                                            .ToListAsync())
                                                                    .Where(Title => Input.LstTranslate.Where(b => b.Title==Title).Any())
                                                                    .Any();
                    if (HasDuplicateTitle)
                        return new OperationResult().Failed("TitleLangIsDuplicate");
                }
                #endregion

                #region Get sorting num
                int _SortNum = 0;
                {
                    _SortNum= await _ShowcaseTabsRepository.Get.Where(a => a.ShowcaseId==Input.ShowcaseId.ToGuid()).CountAsync();
                }
                #endregion

                #region Add showcaseTab
                {
                    var tShowcaseTab = new tblShowcaseTabs
                    {
                        Id= new Guid().SequentialGuid(),
                        ShowcaseId=Input.ShowcaseId.ToGuid(),
                        Name=Input.Name,
                        Sort=_SortNum,
                        BackgroundColorCode=Input.BackgroundColorCode,
                        IsEnable=Input.IsEnable,
                        StartDate=Input.StartDate.Value,
                        EndDate=Input.EndDate,
                        IsActive=Input.StartDate<DateTime.Now ? true : false,
                        tblShowcaseTabTranslates= Input.LstTranslate.Select(a => new tblShowcaseTabTranslates
                        {
                            Id= new Guid().SequentialGuid(),
                            LangId=a.LangId.ToGuid(),
                            Title=a.Title,
                        }).ToList()
                    };

                    await _ShowcaseTabsRepository.AddAsync(tShowcaseTab, default, true);
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

        public async Task<OperationResult> SortingShowcaseTabAsync(InpSortingShowcaseTab Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                #endregion

                var qListShowcaseTab = await _ShowcaseTabsRepository.Get.Where(a => a.ShowcaseId==Input.ShowcaseId.ToGuid()).OrderBy(a => a.Sort).ToListAsync();
                var qCurrentItem = qListShowcaseTab.Where(a => a.Id==Input.Id.ToGuid()).SingleOrDefault();
                if (qCurrentItem==null)
                    return new OperationResult().Failed("IdNotFound");

                int IndexOfCurrentItem = qListShowcaseTab.IndexOf(qCurrentItem);

                if (Input.Act==InpSortingShowcaseTabItem.Up)
                {
                    if (IndexOfCurrentItem!=0)
                    {
                        var PriveItem = qListShowcaseTab[IndexOfCurrentItem-1];

                        int OldPriveIndex = PriveItem.Sort;
                        PriveItem.Sort=IndexOfCurrentItem;
                        qCurrentItem.Sort=OldPriveIndex;

                        await _ShowcaseTabsRepository.UpdateAsync(PriveItem, default, false);
                        await _ShowcaseTabsRepository.UpdateAsync(qCurrentItem, default, false);

                        await _ShowcaseTabsRepository.SaveChangeAsync();
                    }
                }
                else if (Input.Act==InpSortingShowcaseTabItem.Down)
                {
                    if (IndexOfCurrentItem<(qListShowcaseTab.Count()-1))
                    {
                        var NextItem = qListShowcaseTab[IndexOfCurrentItem+1];

                        int OldPriveIndex = NextItem.Sort;
                        NextItem.Sort=IndexOfCurrentItem;
                        qCurrentItem.Sort=OldPriveIndex;

                        await _ShowcaseTabsRepository.UpdateAsync(NextItem, default, false);
                        await _ShowcaseTabsRepository.UpdateAsync(qCurrentItem, default, false);

                        await _ShowcaseTabsRepository.SaveChangeAsync();
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

        public async Task<OperationResult> RemoveShowcaseTabAsync(InpRemoveShowcaseTab Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseTabsRepository.Get
                                                     .Where(a => a.Id==Input.Id.ToGuid())
                                                     .Select(a => new
                                                     {
                                                         HasTabSection = a.tblShowcaseTabSections.Any(),
                                                         ShowcaseTab = a
                                                     })
                                                     .SingleOrDefaultAsync();

                if (qData==null)
                    return new OperationResult().Failed("IdNotFound");

                if (qData.HasTabSection)
                    return new OperationResult().Failed("ShowCaseHasTabSection.PleaseRemove");

                await _ShowcaseTabsRepository.DeleteAsync(qData.ShowcaseTab, default, true);
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

        public async Task<OutGetShowcaseTabForEdit> GetShowcaseTabForEditAsync(InpGetShowcaseTabForEdit Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseTabsRepository.Get
                                                .Where(a => a.Id==Input.Id.ToGuid())
                                                .Select(a => new OutGetShowcaseTabForEdit
                                                {
                                                    Id=a.Id.ToString(),
                                                    ShowcaseId=a.ShowcaseId.ToString(),
                                                    Name=a.Name,
                                                    BackgroundColorCode=a.BackgroundColorCode,
                                                    IsEnable=a.IsEnable,
                                                    StartDate=a.StartDate,
                                                    EndDate=a.EndDate,
                                                    LstTranslate= a.tblShowcaseTabTranslates.Select(b => new OutGetShowcaseTabForEdit_Translate
                                                    {
                                                        LangId=b.LangId.ToString(),
                                                        Title=b.Title
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

        public async Task<OperationResult> SaveEditShowcaseTabAsync(InpSaveEditShowcaseTab Input)
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

                #region Check name duplicate
                {
                    if (await _ShowcaseTabsRepository.Get
                                        .Where(a => a.Id!=Input.Id.ToGuid())
                                        .Where(a => a.ShowcaseId==Input.ShowcaseId.ToGuid())
                                        .AnyAsync(a => a.Name==Input.Name))
                        return new OperationResult().Failed("NameIsDuplicate");
                }
                #endregion

                #region Check title duplicate
                {
                    var HasDuplicateTitle = (await _ShowcaseTabsTranslateRepository.Get
                                                            .Where(a => a.tblShowcaseTabs.Id!=Input.Id.ToGuid())
                                                            .Where(a => a.tblShowcaseTabs.ShowcaseId==Input.ShowcaseId.ToGuid())
                                                            .Select(a => a.Title)
                                                            .ToListAsync())
                                                                    .Where(Title => Input.LstTranslate.Where(b => b.Title==Title).Any())
                                                                    .Any();
                    if (HasDuplicateTitle)
                        return new OperationResult().Failed("TitleLangIsDuplicate");
                }
                #endregion

                #region Edit showcase
                {
                    var qData = await _ShowcaseTabsRepository.Get
                                            .Where(a => a.Id==Input.Id.ToGuid())
                                            .SingleOrDefaultAsync();

                    if (qData==null)
                        return new OperationResult().Failed("IdNotFound");

                    qData.Name=Input.Name;
                    qData.BackgroundColorCode= Input.BackgroundColorCode;
                    qData.EndDate=Input.EndDate;
                    qData.StartDate=Input.StartDate.Value;
                    qData.IsEnable=Input.IsEnable;
                    qData.IsActive=Input.StartDate<DateTime.Now ? true : false;

                    await _ShowcaseTabsRepository.UpdateAsync(qData, default, true);

                }
                #endregion

                #region Edit showcase translate
                {
                    #region Remove old translates
                    {
                        var qOldData = await _ShowcaseTabsTranslateRepository.Get
                                                        .Where(a => a.ShowcaseTabId==Input.Id.ToGuid())
                                                        .ToListAsync();

                        await _ShowcaseTabsTranslateRepository.DeleteRangeAsync(qOldData, default, true);
                    }
                    #endregion

                    #region Add new translates
                    {
                        var qNewData = Input.LstTranslate.Select(b => new tblShowcaseTabTranslates
                        {
                            Id= new Guid().SequentialGuid(),
                            ShowcaseTabId=Input.Id.ToGuid(),
                            LangId=b.LangId.ToGuid(),
                            Title=b.Title,
                        });

                        await _ShowcaseTabsTranslateRepository.AddRangeAsync(qNewData, default, true);
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
    }
}
