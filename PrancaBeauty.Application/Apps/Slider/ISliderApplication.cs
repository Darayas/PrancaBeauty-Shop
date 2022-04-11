using PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Slider
{
    public interface ISliderApplication
    {
        Task<List<OutGetLstSlidesForMainSlider>> GetLstSlidesForMainSliderAsync();
    }
}