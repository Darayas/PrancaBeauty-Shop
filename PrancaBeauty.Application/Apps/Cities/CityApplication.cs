using PrancaBeauty.Domin.Region.CityAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Cities
{
    public class CityApplication : ICityApplication
    {
        private readonly ICityRepository _CityRepository;

        public CityApplication(ICityRepository cityRepository)
        {
            _CityRepository = cityRepository;
        }
    }
}
