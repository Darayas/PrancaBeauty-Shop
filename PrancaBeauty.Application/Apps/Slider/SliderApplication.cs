using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders;
using PrancaBeauty.Domin.Sliders.SliderAgg.Contracts;
using PrancaBeauty.Domin.Sliders.SliderAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Slider
{
    public class SliderApplication : ISliderApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISliderRepository _SliderRepository;

        public SliderApplication(ILogger logger, IServiceProvider serviceProvider, ISliderRepository sliderRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _SliderRepository=sliderRepository;
        }

        public async Task<List<OutGetLstSlidesForMainSlider>> GetLstSlidesForMainSliderAsync()
        {
            try
            {
                #region Validations

                #endregion

                var qData = await _SliderRepository.Get
                                                   .Where(a => a.IsEnable)
                                                   .Where(a => a.IsActive)
                                                   .OrderByDescending(a => a.Sort)
                                                   .Select(a => new OutGetLstSlidesForMainSlider
                                                   {
                                                       Title=a.Title,
                                                       Url=a.Url,
                                                       IsFollow=a.IsFollow,
                                                       ImgUrl = a.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                + a.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                + a.tblFiles.tblFilePaths.Path
                                                                + a.tblFiles.FileName
                                                   })
                                                   .ToListAsync();

                return qData;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<(OutPagingData Paging, List<OutGetListSlideForManage> LstSlider)> GetListSlideForManageAsync(InpGetListSlideForManage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _SliderRepository.Get
                                             .Where(a => Input.Title!=null ? a.Title.Contains(Input.Title) : true)
                                             .Select(a => new OutGetListSlideForManage
                                             {
                                                 Id=a.Id.ToString(),
                                                 Title=a.Title,
                                                 Url=a.Url,
                                                 Sort=a.Sort,
                                                 IsActive=a.IsActive,
                                                 IsEnable=a.IsEnable,
                                                 StartDate=a.StartDate,
                                                 EndDate=a.EndDate,
                                                 Date=a.Date,
                                                 ImgUrl= a.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                          + a.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                          + a.tblFiles.tblFilePaths.Path
                                                          + a.tblFiles.FileName
                                             })
                                             .OrderBy(a => a.Sort);

                var _PagingData = PagingData.Calc(await qData.CountAsync(), Input.CurrentPage, Input.Take);
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

        public async Task<OperationResult> RemoveSliderAsync(InpRemoveSlider Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _SliderRepository.Get.Where(a => a.Id==Input.Id.ToGuid()).SingleOrDefaultAsync();
                if (qData==null)
                    return new OperationResult().Failed("IdNotFound");

                await _SliderRepository.DeleteAsync(qData, default, true);

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> SortingSliderAsync(InpSortingSlider Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qListSlide = await _SliderRepository.Get.OrderBy(a => a.Sort).ToListAsync();
                var qCurrentItem = qListSlide.Where(a => a.Id==Input.Id.ToGuid()).SingleOrDefault();
                if (qCurrentItem==null)
                    return new OperationResult().Failed("IdNotFound");

                int IndexOfCurrentItem = qListSlide.IndexOf(qCurrentItem);

                if (Input.Action==InpSortingSliderItem.Up)
                {
                    if (IndexOfCurrentItem!=0)
                    {
                        var PriveItem = qListSlide[IndexOfCurrentItem-1];

                        int OldPriveIndex = PriveItem.Sort;
                        PriveItem.Sort=IndexOfCurrentItem;
                        qCurrentItem.Sort=OldPriveIndex;

                        await _SliderRepository.UpdateAsync(PriveItem, default, false);
                        await _SliderRepository.UpdateAsync(qCurrentItem, default, false);

                        await _SliderRepository.SaveChangeAsync();
                    }
                }
                else if (Input.Action==InpSortingSliderItem.Down)
                {
                    if (IndexOfCurrentItem<(qListSlide.Count()-1))
                    {
                        var NextItem = qListSlide[IndexOfCurrentItem+1];

                        int OldPriveIndex = NextItem.Sort;
                        NextItem.Sort=IndexOfCurrentItem;
                        qCurrentItem.Sort=OldPriveIndex;

                        await _SliderRepository.UpdateAsync(NextItem, default, false);
                        await _SliderRepository.UpdateAsync(qCurrentItem, default, false);

                        await _SliderRepository.SaveChangeAsync();
                    }
                }


                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> AddSlideAsync(InpAddSlide Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                if (Input.StartDate==null && Input.EndDate!=null)
                    Input.StartDate=DateTime.Now;


                if (Input.StartDate.HasValue && Input.EndDate.HasValue)
                    if (Input.StartDate.Value >= Input.EndDate.Value)
                        return new OperationResult().Failed("EndDateMustBeGreaterThanStartDate");
                #endregion

                #region Check Title Duplicate
                {
                    var IsDuplicate = await _SliderRepository.Get.AnyAsync(a => a.Title==Input.Title);
                    if (IsDuplicate)
                        return new OperationResult().Failed("TitleIsDuplicated");
                }
                #endregion

                #region Get SortNum
                int _Sort = 0;
                {
                    _Sort= await _SliderRepository.Get.CountAsync();
                }
                #endregion

                #region Add Slide
                var tSlide = new tblSlider
                {
                    Id=new Guid().SequentialGuid(),
                    UserId=Input.UserId.ToGuid(),
                    Title=Input.Title,
                    Date=DateTime.Now,
                    FileId=Input.FileId.ToGuid(),
                    Url=Input.Url,
                    Sort=_Sort,
                    IsEnable=Input.IsEnable,
                    IsFollow=Input.IsFollow,
                    StartDate=Input.StartDate,
                    EndDate=Input.EndDate,
                    IsActive=Input.StartDate==null ? true : (Input.StartDate.Value<=DateTime.Now ? true : false)
                };

                await _SliderRepository.AddAsync(tSlide, default, true);
                #endregion

                return new OperationResult().Succeeded();
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

        public async Task<OutGetSlideForEdit> GetSlideForEditAsync(InpGetSlideForEdit Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _SliderRepository.Get.Where(a => a.Id==Input.Id.ToGuid())
                                                   .Select(a => new OutGetSlideForEdit
                                                   {
                                                       Id=a.Id.ToString(),
                                                       Title=a.Title,
                                                       StartDate=a.StartDate,
                                                       EndDate=a.EndDate,
                                                       Url=a.Url,
                                                       FileId=a.FileId.ToString(),
                                                       IsEnable=a.IsEnable,
                                                       IsFollow=a.IsFollow
                                                   })
                                                   .SingleOrDefaultAsync();

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

        public async Task<OperationResult> SaveEditSlideAsync(InpSaveEditSlide Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                if (Input.StartDate==null && Input.EndDate!=null)
                    Input.StartDate=DateTime.Now;


                if (Input.StartDate.HasValue && Input.EndDate.HasValue)
                    if (Input.StartDate.Value >= Input.EndDate.Value)
                        return new OperationResult().Failed("EndDateMustBeGreaterThanStartDate");
                #endregion

                #region Check Title Duplicate
                {
                    var IsDuplicate = await _SliderRepository.Get.Where(a => a.Id!=Input.Id.ToGuid()).AnyAsync(a => a.Title==Input.Title);
                    if (IsDuplicate)
                        return new OperationResult().Failed("TitleIsDuplicated");
                }
                #endregion

                #region Edit Slide
                {
                    var qData = await _SliderRepository.Get.Where(a => a.Id==Input.Id.ToGuid()).SingleOrDefaultAsync();
                    if (qData==null)
                        return new OperationResult().Failed("IdNotFound");

                    qData.Title = Input.Title;
                    qData.FileId=Input.FileId.ToGuid();
                    qData.Url=Input.Url;
                    qData.IsFollow= Input.IsFollow;
                    qData.IsEnable=Input.IsEnable;
                    qData.StartDate=Input.StartDate;
                    qData.EndDate=Input.EndDate;
                    qData.IsActive=Input.StartDate==null ? true : (Input.StartDate.Value<=DateTime.Now ? true : false);

                    await _SliderRepository.UpdateAsync(qData, default, true);
                }
                #endregion

                return new OperationResult().Succeeded();

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
