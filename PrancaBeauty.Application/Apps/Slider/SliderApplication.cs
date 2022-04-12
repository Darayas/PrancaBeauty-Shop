using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders;
using PrancaBeauty.Domin.Sliders.SliderAgg.Contracts;
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
    }
}
