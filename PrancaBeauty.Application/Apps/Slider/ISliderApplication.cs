using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Slider
{
    public interface ISliderApplication
    {
        Task<OperationResult> AddSlideAsync(InpAddSlide Input);
        Task<(OutPagingData Paging, List<OutGetListSlideForManage> LstSlider)> GetListSlideForManageAsync(InpGetListSlideForManage Input);
        Task<List<OutGetLstSlidesForMainSlider>> GetLstSlidesForMainSliderAsync();
        Task<OutGetSlideForEdit> GetSlideForEditAsync(InpGetSlideForEdit Input);
        Task<OperationResult> RemoveSliderAsync(InpRemoveSlider Input);
        Task<OperationResult> SaveEditSlideAsync(InpSaveEditSlide Input);
        Task<OperationResult> SortingSliderAsync(InpSortingSlider Input);
    }
}