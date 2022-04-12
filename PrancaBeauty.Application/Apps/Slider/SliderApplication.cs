using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
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

        public async Task<string> GetListSlideForManageAsync(InpGetListSlideForManage Input)
        {
            try
            {
                #region Validations

                #endregion

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
