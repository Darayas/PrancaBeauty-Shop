using Framework.Infrastructure;
using PrancaBeauty.Domin.Sliders.SliderAgg.Contracts;
using PrancaBeauty.Domin.Sliders.SliderAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Slider
{
    public class SliderRepository : BaseRepository<tblSlider>, ISliderRepository
    {
        public SliderRepository(MainContext Context) : base(Context)
        {

        }
    }
}
