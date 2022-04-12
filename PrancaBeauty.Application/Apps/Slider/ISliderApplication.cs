using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Slider
{
    public interface ISliderApplication
    {
        Task<(OutPagingData Paging, List<OutGetListSlideForManage> LstSlider)> GetListSlideForManageAsync(InpGetListSlideForManage Input);
        Task<List<OutGetLstSlidesForMainSlider>> GetLstSlidesForMainSliderAsync();
    }
}